FROM pandoc/typst:latest-ubuntu

# Install .NET 10 SDK
RUN apt-get update && apt-get install -y \
    wget \
    ca-certificates \
    apt-transport-https \
    && rm -rf /var/lib/apt/lists/*

RUN wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh && \
    chmod +x dotnet-install.sh && \
    ./dotnet-install.sh --channel 10.0 --install-dir /usr/share/dotnet && \
    ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

WORKDIR /app
COPY . .

RUN mkdir -p /app/output

ENTRYPOINT ["dotnet", "run", "app.cs"]