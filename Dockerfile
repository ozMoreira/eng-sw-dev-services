# ─── STAGE 1: BUILD & PUBLISH ────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:9.0.100 AS build
WORKDIR /src

# 1) Copia TODO o código de uma vez
COPY . .

# 2) Restaura cada projeto
RUN dotnet restore "users-microservice/users-microservice/users-microservice.csproj"
RUN dotnet restore "orders-microservice/orders-microservice/orders-microservice.csproj"
RUN dotnet restore "products-microservice/products-microservice/products-microservice.csproj"

# 3) Publica em Release (já faz build + otimizações)
RUN dotnet publish "users-microservice/users-microservice/users-microservice.csproj" \
    -c Release -o /app/publish/users /p:UseAppHost=false

RUN dotnet publish "orders-microservice/orders-microservice/orders-microservice.csproj" \
    -c Release -o /app/publish/orders /p:UseAppHost=false

RUN dotnet publish "products-microservice/products-microservice/products-microservice.csproj" \
    -c Release -o /app/publish/products /p:UseAppHost=false

# ─── STAGE 2: RUNTIME ────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:9.0.100 AS runtime
WORKDIR /app

# Exponha as portas que cada serviço usa
EXPOSE 8080 8081 8082

# 4) Copia só o que foi publicado
COPY --from=build /app/publish/users    ./users
COPY --from=build /app/publish/orders   ./orders
COPY --from=build /app/publish/products ./products

# 5) Script que inicia todos em paralelo
COPY entrypoint.sh .
# Remove CR (\r) de finais de linha e torna executável
RUN sed -i 's/\r$//' entrypoint.sh \
 && chmod +x entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]
