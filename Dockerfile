# 1. AŞAMA: İNŞA ETME (BUILD)
# Microsoft'un hazır .NET 8 kutusunu kullanıyoruz
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Proje dosyalarını kopyala
COPY . .

# Gerekli kütüphaneleri yükle (Restore)
RUN dotnet restore

# Projeyi yayınlanabilir hale getir (Publish)
RUN dotnet publish -c Release -o out

# 2. AŞAMA: ÇALIŞTIRMA (RUN)
# Sadece çalıştırma dosyalarını alan daha hafif bir kutuya geçiyoruz
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# İlk aşamada ürettiğimiz dosyaları buraya alıyoruz
COPY --from=build /app/out .

# Veritabanı dosyasını da kopyaladığımızdan emin olalım (SQLite için kritik)
# Eğer db dosyası kodların arasındaysa yukarıdaki COPY komutu zaten alır.

# Uygulamanın çalışacağı portu belirtelim (Render genelde bunu sever)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# SON KOMUT: Projeyi Başlat!
# DİKKAT: Buradaki isim projenin .csproj dosyasıyla aynı olmalı.
ENTRYPOINT ["dotnet", "HediyelikEsya.dll"]
