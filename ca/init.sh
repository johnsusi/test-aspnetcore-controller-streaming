#!/usr/bin/env bash

src="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

mkdir -p /var/lib/certs

pushd /var/lib/certs

cfssl gencert -initca $src/csr/root.json | cfssljson -bare root
cfssl genkey -initca $src/csr/intermediate.json | cfssljson -bare intermediate
cfssl sign -ca root.pem -ca-key root-key.pem --config $src/config.json -profile intermediate intermediate.csr | cfssljson -bare intermediate
cat root.pem intermediate.pem > ca-bundle.pem

cfssl gencert -ca intermediate.pem -ca-key intermediate-key.pem -config $src/config.json -profile server $src/csr/back.json | cfssljson -bare back
openssl pkcs12 -export -inkey back-key.pem -name back -in back.pem -certfile ca-bundle.pem -passout pass: -out back.p12

cfssl gencert -ca intermediate.pem -ca-key intermediate-key.pem -config $src/config.json -profile server $src/csr/influxdb.json | cfssljson -bare influxdb
openssl pkcs12 -export -inkey influxdb-key.pem -name influxdb -in influxdb.pem -certfile ca-bundle.pem -passout pass: -out influxdb.p12


popd

