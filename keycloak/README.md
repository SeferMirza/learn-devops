# Keycloak

Buradaki dokumantasyon keycloak'un docker ile kullanımına yöneliktir.

## Configuration

4 yöntem ile keycloak konfigüre edilebilir.

1. Command-line parameters
1. Environment variables
1. Options defined in the conf/keycloak.conf file, or in a user-created configuration file.
1. Sensitive options defined in a user-created Java KeyStore file.

Örneklere configuration file ile devam edeceğiz.

config dosyasına configler <key-with-dashes>=<value> şeklinde eklenir.

default config dosya path conf/keycloak.conf tir

ayrıca You can use placeholders to resolve an environment specific value from environment variables inside the keycloak.conf file by using the ${ENV_VAR} syntax:

db-url-host=${MY_DB_HOST}

In case the environment variable cannot be resolved, you can specify a fallback value. Use a : (colon) as shown here before mydb:

db-url-host=${MY_DB_HOST:mydb}

> :Info:
>
> Special Characters
>
> \ character functions as an escape character
> $ characters when they appear to define an expression or are repeated

bütün configler için https://www.keycloak.org/server/all-config?f=build

> :Info:
>
> run sırasında bazı realm ayarlarına izin verilmez ama spi-admin--allowed-system-variables gibi flaglar ile
> izin verdirilebilir.

### db

db ekleme için config dosyasına db-url-host=mykeycloakdb eklenir

### Quarkus framework

Keycloak yapılandırmasında eksik olan belirli bir davranış veya yetenek için,
altta yatan Quarkus çerçevesinin özelliklerini kullanabilirsiniz.
detay için https://www.keycloak.org/server/configuration#_format_for_raw_quarkus_properties

## Modes

Keycloak development ve production modlarında çalışabilir. Default modu development modudur.
bir kaç özellik bu modda disabledır. detay için https://www.keycloak.org/server/configuration#_starting_keycloak_in_development_mode

### Production mode

Production mode da başlatmak için bazı ayarlamaya ihtiyaç duyuyor. bunlar

- HTTP is disabled as transport layer security (HTTPS) is essential
- Hostname configuration is expected
- HTTPS/TLS configuration is expected

## Optimizations

Dockerda kullanmayı planladığımız için ayağa kaldırma süresini optimize etmemiz gerekiyor.
Bunun için Keycloak tarafından önerilen bazı optimizasyonlar var.

1. Normal build yap
2. --optimized flag ile başlat

--optimized flag zaten build alındı sen önceki build i kullan demek.

eğer --optimized flag ile başlatılan uygulamaya build config verilirse ve hali
hazırda pre-build te bu verilmişse run sırasında verilen ignore edilir

## UI

{base-url}/admin adresine giderek keycloak yönetim arayüzüne erişilebilir.
Burada realm ve user ekleme silme gibi bir çok işlem yapılabiliyor.

## API

Ayakta olan keycloak servisini api ile yönetebiliyoruz. bu api lere bir örnek olarak
POST /admin/realms/{realm}/logout-all
verilebilir. Apiler {base-url}/admin/ ile başlar. Bütün api leri görmek için
https://www.keycloak.org/docs-api/latest/rest-api/index.html adresine bakılabilir.

## Realms

Keycloak'ta alanlar üzerinden yönetim yapılır. Her alan kendi kullanıcıları tutar.
Başlangıçta bir adet "master" alanı vardır. Önerilen master alanını sadece keycloak'ı yönetmek için kullanmaktır.

## Docker

docker ile kullanımda local için pek olasada prod ortamlarında iyi memory
ayırlamaları yapmak gerekiyor.

Memory hesaplamaları için
https://www.keycloak.org/high-availability/single-cluster/concepts-memory-and-cpu-sizing#single-cluster-single-site-calculation
bakılabilir.