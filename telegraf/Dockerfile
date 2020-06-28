FROM telegraf

COPY telegraf.conf /etc/telegraf/telegraf.conf

COPY ca-bundle.pem /usr/local/share/ca-certificates/ca-bundle.crt

RUN update-ca-certificates


