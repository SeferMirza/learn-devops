FROM quay.io/keycloak/keycloak:latest

COPY /keycloak/keycloak.conf /opt/keycloak/conf/keycloak.conf

ENTRYPOINT ["/opt/keycloak/bin/kc.sh"]
CMD ["start-dev"]
