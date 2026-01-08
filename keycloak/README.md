# Keycloak

This documentation covers using Keycloak with Docker in this repository.

At the core of Keycloak, there are realms that represent systems at the root
level. Under these realms, there are clients, users, and integrations.

Here, only the steps required for the user to log in, obtain an `auth code` and
generate a token with this code are shortly documented.

## Structure

We use Keycloak in our projects in the following structure:

```
project-root/
 ├── keycloak/
 │   ├── Dockerfile
 │   └── keycloak.conf
 ├── compose.yml
 └── ...
```

### Compose setup

The following structure can be followed for a simple setup with a compose file.

```yml
services:
  db:
    image: db.image
    environment:
      DB: db
      USER: db.user
      ...
    ...

  keycloak:
    build:
      dockerfile: keycloak/Dockerfile
    environment:
      - KC_DB_URL_DATABASE=db
      - KC_DB_USERNAME=db.user
      ...
    ...
```

Configurations provided for configuring Keycloak can be specified in the
environment as shown above, or they can also be provided in a `.conf` file in
the format `<key-with-dashes>=<value>`.

For a detailed example, see `compose.yml`.

For more information on configuration options and setup, see [All Configs].

### Dockerfile

In the Dockerfile, we only pull the image, copy the `.conf` file and realm files
to their directories, and run it with kc.sh start.

Important points:

- `start` runs in production mode, `start-dev` runs in development mode
- Additional configurations can be provided while running. The priority order in
  the configurations is `cli>env>conf`.
- Production requires additional setup:
  - HTTP disabled; HTTPS (TLS) required
  - Hostname configuration required
  - HTTPS/TLS configuration required

See [Dockerfile](keycloak/Dockerfile) for an example.

#### One time import realms

You can create realms via the UI or Admin API, or import a prebuilt JSON by
copying it into the image under `/opt/keycloak/data/import/` and starting with
realm import enabled.

This import works only if the realms not exist. It cannot be used for data
updates.

## Optimizations

For faster startup in containers, use the recommended flow:

1. Build once normally
2. Start with `--optimized` to reuse the build

If runtime build config conflicts with a pre-build, the pre-built assets take
precedence.

## Keycloak Administration

Keycloak creates a realm named `master` by default in first start. An admin
login is required to perform operations in this master realm. We provide these
user credentials via the environment as `KC_BOOTSTRAP_ADMIN_USERNAME` and
`KC_BOOTSTRAP_ADMIN_PASSWORD`. This user is opened as a temporary user. It is
not recommended to continue using this user.

### Realms

Realms isolate users and configuration. The master realm is designated as the
administrator realm. If a client and a regular user are to be added, a separate
realm must be created.

To create a new realm,

1. login using the admin user under the master realm (assuming it is newly
  created, this will be the one you created with `KC_BOOTSTRAP_ADMIN_X`).
1. You can create a new realm by simply entering a `name` and selecting `enable`
  on the manage realm page, accessible from the side menu.

### Clients

After creating a realm, I assume you automatically enter that realm. At this
point, clients will be created within whichever realm you are currently in.

To create a client:

1. Open the new client creation screen from the Clients tab in the side menu
1. Enter a `Client ID` and ensure that the `Client Protocol` is set to
  `openid-connect`. Then click next
1. Make sure `client authentication` is enabled, then you can click next.
1. After entering the `Root URL`, please enter `Valid redirect URIs`,
  considering the possible redirect URLs as well. It is important to note that
  after the root URL is provided, it is added to the beginning of the redirect
  URL. If the redirect URL provided for the auth code is not within the valid
  scope, it is blocked
1. Now you can save it

When API requests are sent through this client, the `client_secret` must be
provided. You can find this secret information in the credentials section of the
Clients page.

### Users

User creation can also be easily done by going to their own page. However, the
user's password is set by going to the user's page after the user is created and
doing it from the `Credentials` tab.

## Login Flows

### User Login

The user's authentication screen will be explained below.

The user is redirected to
`http://keycloak/realms/<realm-name>/protocol/openid-connect/auth` to log in.
This process requires the query parameters provided below with their
descriptions.

Query parameters:
- `client_id`: The client ID (e.g. weather-api)
- `response_type`: `code` (for Authorization Code Flow)
- `scope`: `openid` (and any additional scopes)
- `redirect_uri`: Where the user will be redirected after login

After the user login, they are redirected to the `redirect_uri` URL we provided.
Keycloak sends the state and auth code information in the query during this
redirection process.

After the Keycloak login process, it could have redirected directly to the token
instead of the auth code. However, since it sends this token in the query, it is
not secure. Obtaining a one-time code would be the most logical approach.

### Getting Token

After the code is obtained, a token will be required for other operations. To
obtain this token, we send a request to
`http://keycloak/realms/<realm-name>/protocol/openid-connect/token`. The
required body parameters for this request are listed below.

```
Content-Type: application/x-www-form-urlencoded

client_id=weather-api
grant_type=authorizationCode
code=authCode
client_secret=clientSecret
redirect_uri=redirectUri
```

The important thing here is that the parameters used when obtaining the code
must be the same as those used when obtaining the token. For example,
`redirect_uri`

Since we use our own tokens in our projects, we pull the code received by the
user into our service and go to Keycloak with the service to obtain a token. We
retrieve the user information within this token, assign it to our own claims,
and create our own token.

## References

[All Configs]: https://www.keycloak.org/server/all-config