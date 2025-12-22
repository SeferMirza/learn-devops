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
- Escaping: `\` escapes characters; `$` is special in expressions

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

## Docker

For production, size memory appropriately. Guidance: [Sizing Guide]

## References

[All Configs]: https://www.keycloak.org/server/all-config?f=build
[Quarkus Properties]: https://www.keycloak.org/server/configuration#_format_for_raw_quarkus_properties
[Dev Mode]: https://www.keycloak.org/server/configuration#_starting_keycloak_in_development_mode
[API Reference]: https://www.keycloak.org/docs-api/latest/rest-api/index.html
[OIDC Discovery]: http://localhost:8080/realms/master/.well-known/openid-configuration
[Sizing Guide]: https://www.keycloak.org/high-availability/single-cluster/concepts-memory-and-cpu-sizing#single-cluster-single-site-calculation