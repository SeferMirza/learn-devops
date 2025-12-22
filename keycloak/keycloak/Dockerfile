FROM quay.io/keycloak/keycloak:latest

COPY keycloak.conf /opt/keycloak/conf/keycloak.conf
COPY realm-config.json /opt/keycloak/data/import/

ENTRYPOINT ["/opt/keycloak/bin/kc.sh"]
CMD ["start-dev", "--import-realm"]
