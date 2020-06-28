FROM envoyproxy/envoy-dev:latest
# FROM envoyproxy/envoy:v1.14.1

COPY *.crt /etc/ssl/certs/

# COPY ca-bundle.crt /usr/local/share/ca-certificates/
# RUN update-ca-certificates

COPY ./envoy.yaml /etc/envoy/envoy.yaml

EXPOSE 8080

CMD /usr/local/bin/envoy -c /etc/envoy/envoy.yaml -l warning