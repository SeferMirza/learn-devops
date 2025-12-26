# Keycloak

This documentation covers using Keycloak with Docker in this repository.

At the core of Keycloak, there are realms that represent systems at the root
level. Under these realms, there are clients, users, and integrations.

The scenario here is that the user login to the system via keycloak and get an
`auth code` then get a token from our service to gain access to the service.

## Setup

Since it is part of a system, it is removed in the same compose file as the
project. Since it can have its own config file, Keycloak has its own folder, and
within this folder, there is a Dockerfile and a config file.

Keycloak also needs a database to work. It would be best if this database had
its own separate database or schema.

### Configuration

Keycloak can be configured in four ways:

1. Command-line parameters
2. Environment variables
3. Options in `conf/keycloak.conf` (or a user-provided config file)
4. Sensitive options in a Java KeyStore

We use the config file approach. Options follow the format
`<key-with-dashes>=<value>`.

- Default config path: `conf/keycloak.conf`
- Environment placeholders: `${ENV_VAR}` with optional fallback
  `${ENV_VAR:default}`

All available options: [All Configs]

Note: Some realm settings are restricted at runtime; enabling flags (e.g.,
`spi-admin-allowed-system-variables`) should be used cautiously.

#### Database

You can configure the database via the config file, environment variables, or
CLI flags. Precedence: CLI > environment > config file.

Using `keycloak.conf`:

```
db-url-host=mykeycloakdb
```

#### Quarkus framework

For gaps in Keycloak options, you can fall back to raw Quarkus properties:
[Quarkus Properties]

### One time import realms

Keycloak creates a `master` realm by default; avoid using it for applications.
You can create realms via the UI or Admin API, or import a prebuilt JSON by
copying it into the image under `/opt/keycloak/data/import/` and starting with
realm import enabled.

This import works only if the realms not exist. It cannot be used for data
updates.

## Modes

Keycloak runs in development (default) and production modes. Some features
differ: [Dev Mode]

### Production mode

Production requires additional setup:

- HTTP disabled; HTTPS (TLS) required
- Hostname configuration required
- HTTPS/TLS configuration required

## Optimizations

For faster startup in containers, use the recommended flow:

1. Build once normally
2. Start with `--optimized` to reuse the build

If runtime build config conflicts with a pre-build, the pre-built assets take
precedence.

## UI

Access the Admin Console at `{base-url}/admin` to manage realms, users, and
settings.

## Realms

Realms isolate users and configuration. The `master` realm exists by default and
should be used only for administering Keycloak.

## Login Flows

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

[All Configs]: https://www.keycloak.org/server/all-config?f=build
[Quarkus Properties]: https://www.keycloak.org/server/configuration#_format_for_raw_quarkus_properties
[Dev Mode]: https://www.keycloak.org/server/configuration#_starting_keycloak_in_development_mode
[API Reference]: https://www.keycloak.org/docs-api/latest/rest-api/index.html
[OIDC Discovery]: http://localhost:8080/realms/master/.well-known/openid-configuration