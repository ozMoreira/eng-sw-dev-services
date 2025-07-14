#!/usr/bin/env bash
set -e

# inicia cada serviço em background
dotnet ./users/users-microservice.dll  &
dotnet ./orders/orders-microservice.dll &
dotnet ./products/products-microservice.dll &

# mantém o container vivo até qualquer um terminar
wait -n
