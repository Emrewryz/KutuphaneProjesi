# Kütüphane Yönetim Sistemi

Bu proje, ASP.NET Core 8 MVC ve SQL Server kullanılarak geliştirilmiş bir staj projesidir. Proje, temel bir kütüphanenin kitap, yazar, kategori ve kullanıcı yönetimi işlevlerini içerir. Rol bazlı yetkilendirme (Admin/Üye) ile güvenliği sağlanmış, modern ve dinamik bir web uygulamasıdır.

## Kullanılan Teknolojiler
* **Backend:** ASP.NET Core 8 MVC (C#)
* **Veritabanı:** Microsoft SQL Server
* **Veri Erişimi:** Entity Framework Core 8 (ORM)
* **Frontend:** HTML5, CSS3, Bootstrap 5 & Bootstrap Icons
* **Güvenlik:** Sıfırdan Geliştirilmiş Cookie Tabanlı Kimlik Doğrulama

## Proje Özellikleri
* **Yönetim Paneli (Admin):**
    * Kitap, yazar ve kategori yönetimi (CRUD).
    * Sisteme kayıtlı kullanıcıları listeleme, rol atama (Admin/Üye) ve silme.
* **Kullanıcı Arayüzü (Üye & Ziyaretçi):**
    * Kitapları modern kart tasarımında listeleme.
    * Kitap adına göre arama yapma.
    * Kenar menüden kategoriye göre filtreleme.
    * Kitap detaylarını ve içeriğini görüntüleme.
* **Dinamik Tasarım:**
    * Kullanıcının rolüne göre dinamik olarak değişen menüler ve butonlar.
    * Giriş/Kayıt sayfaları için özel, menüsüz layout.
    * Yeniden kullanılabilir arayüz bileşenleri (Partial Views & View Components).

## Kurulum Adımları

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyin:

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/Emrewryz/KutuphaneProjesi.git](https://github.com/Emrewryz/KutuphaneProjesi.git)
    ```
2.  **Veritabanını Oluşturun:**
    * Microsoft SQL Server Management Studio'yu (SSMS) açın.
    * `KutuphaneDB` adında yeni, boş bir veritabanı oluşturun.
    * Proje içindeki `DatabaseScripts/1_Schema.sql` dosyasının içeriğini bu yeni veritabanı üzerinde çalıştırarak tabloları oluşturun.

3.  **Bağlantı Cümlesini (Connection String) Ayarlayın:**
    * Proje içindeki `appsettings.json` dosyasını açın.
    * `ConnectionStrings` bölümündeki `"DefaultConnection"` değerini kendi SQL Server bilgilerinize göre güncelleyin. Özellikle `Server=...` kısmını kendi sunucu adınızla değiştirin.

4.  **Uygulamayı Çalıştırın:**
    * Projeyi Visual Studio'da açın ve `F5` tuşuna basarak çalıştırın.
    * **Admin Kullanıcısı Oluşturma:** Uygulama üzerinden normal bir kullanıcı kaydı oluşturduktan sonra, SSMS'te aşağıdaki komutu kendi e-postanızla güncelleyerek çalıştırın:
        ```sql
        UPDATE Kullanicilar SET Rol = 'Admin' WHERE Eposta = 'sizin-epostaniz@email.com';
        ```