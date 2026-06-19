FROM mcr.microsoft.com/dotnet/sdk:10.0

# Base tools, Pandoc and Typst
RUN apt-get update && apt-get install -y \
    wget \
    ca-certificates \
    xz-utils \
    && rm -rf /var/lib/apt/lists/*

RUN wget -O /tmp/pandoc.tar.gz \
    https://github.com/jgm/pandoc/releases/download/3.10/pandoc-3.10-linux-amd64.tar.gz && \
    tar -xzf /tmp/pandoc.tar.gz -C /tmp && \
    mv /tmp/pandoc-3.10/bin/pandoc /usr/local/bin/pandoc && \
    chmod +x /usr/local/bin/pandoc && \
    rm -rf /tmp/pandoc*

RUN wget -O /tmp/typst.tar.xz \
    https://github.com/typst/typst/releases/download/v0.15.0/typst-x86_64-unknown-linux-musl.tar.xz && \
    mkdir -p /tmp/typst && \
    tar -xf /tmp/typst.tar.xz -C /tmp/typst --strip-components=1 && \
    mv /tmp/typst/typst /usr/local/bin/typst && \
    chmod +x /usr/local/bin/typst && \
    rm -rf /tmp/typst*

# -------------------------

WORKDIR /app
COPY . .

RUN mkdir -p /app/output

ENTRYPOINT ["dotnet", "run", "app.cs"]