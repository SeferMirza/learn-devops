# Keycloak

This documentation covers using Keycloak with Docker in this repo.

## Configuration

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

### Database

You can configure the database via the config file, environment variables, or
CLI flags. Precedence: CLI > environment > config file.

Using `keycloak.conf`:

```
db-url-host=mykeycloakdb
```

### Quarkus framework

For gaps in Keycloak options, you can fall back to raw Quarkus properties:
[Quarkus Properties]

## Import realms

Keycloak creates a `master` realm by default; avoid using it for applications.
You can create realms via the UI or Admin API, or import a prebuilt JSON by
copying it into the image under `/opt/keycloak/data/import/` and starting with
realm import enabled.

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

## API

Manage Keycloak via the Admin REST API under `{base-url}/admin`.
Example: `POST /admin/realms/{realm}/logout-all`
API reference: [API Reference]

OpenID Connect discovery: [OIDC Discovery]

## Realms

Realms isolate users and configuration. The `master` realm exists by default and
should be used only for administering Keycloak.

## Token

Use Protocol Mappers to add claims to tokens. For example, adding an audience
claim uses the `oidc-audience-mapper`. See `keycloak/realm-config.json` in this
repo for basic examples.

## Login and Redirect

To start the login flow, send a GET request to:

```
GET http://localhost:8080/realms/<realm-name>/protocol/openid-connect/auth
```

Query parameters:
- `client_id`: The client ID (e.g. weather-api)
- `response_type`: `code` (for Authorization Code Flow)
- `scope`: `openid` (and any additional scopes)
- `redirect_uri`: Where the user will be redirected after login
  (e.g. http://localhost)
- `prompt`: (optional) Controls whether the login screen is shown. Use
  `prompt=none` to attempt silent authentication (no UI); if the user is not
  already logged in, an error is returned instead of showing the login page.
  This is useful for checking session status or implementing silent SSO.

Request:
```url
http://localhost:8080/realms/test-realm/protocol/openid-connect/auth
  ?client_id=weather-api
  &response_type=code
  &scope=openid
  &redirect_uri=http://localhost
  &prompt=none
```

After successful login, Keycloak redirects to:
```
http://localhost/?code=AUTH_CODE&session_state=...&iss=...
```

To exchange the `code` for an access token, send a POST request to:
```
POST http://localhost:8080/realms/<realm-name>/protocol/openid-connect/token
Content-Type: application/x-www-form-urlencoded

client_id=weather-api
&grant_type=authorization_code
&code=AUTH_CODE
&redirect_uri=http://localhost
```

#### Redirect URI Settings

For the redirect to work, the client must have:
- `redirectUris`: Allowed redirect URIs (e.g. ["http://localhost/*"])
- `webOrigins`: Allowed CORS origins (e.g. ["http://localhost"])
- `standardFlowEnabled`: true (required for Authorization Code Flow)

> [Info]
>
> If you want to obtain tokens directly from a frontend (SPA/JS) app, set
> `publicClient: true` and do not send `client_secret` in the token request. For
> confidential clients (`publicClient: false`), using the secret in the frontend
> is insecure and required by Keycloak.

These settings must be present in both JSON imports and in the Keycloak UI
client configuration.

## Docker

For production, size memory appropriately. Guidance: [Sizing Guide]

## References

[All Configs]: https://www.keycloak.org/server/all-config?f=build
[Quarkus Properties]: https://www.keycloak.org/server/configuration#_format_for_raw_quarkus_properties
[Dev Mode]: https://www.keycloak.org/server/configuration#_starting_keycloak_in_development_mode
[API Reference]: https://www.keycloak.org/docs-api/latest/rest-api/index.html
[OIDC Discovery]: http://localhost:8080/realms/master/.well-known/openid-configuration
[Sizing Guide]: https://www.keycloak.org/high-availability/single-cluster/concepts-memory-and-cpu-sizing#single-cluster-single-site-calculation