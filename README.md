# AcademicWordNet.MvcCoreWebUI

AcademicWordNet.MvcCoreWebUI is a comprehensive dictionary website designed specifically for academic communities, with a special focus on students within educational institutions. The platform offers functionalities similar to eksisozluk.com but with added features tailored to the academic environment.

## Key Features

- **Student Exclusivity**: Access and contributions are restricted to registered students only.
- **Email Password Reset**: Students can reset their passwords via email.
- **Admin Panel**: Administrators can manage students, perform bulk registration via Excel files, and automatically generate login codes.
- **Personalized Access**: Each student is provided with a unique login code for personalized access.
- **Profanity Filter**: The platform includes a profanity filter in the admin panel to prevent the use of inappropriate language.
- **Topic Approval**: Students can suggest topics, but entries require admin approval before becoming visible.
- **Entry Management**: Students can freely submit entries under approved topics.

## Getting Started

To start using AcademicWordNet.MvcCoreWebUI, follow these steps:

1. Clone this repository to your local machine.
2. Open the solution in Visual Studio or your preferred IDE.
3. Configure your database connection string in the `context.cs` file.
4. Configure the necessary mail information for the mail service in the `appsettings.json` file.
5. Run database migrations to create the necessary tables.
6. Build and run the application.
7. As an administrator, manage student registrations and oversee content quality through the admin panel.

## Türkçe

# AcademicWordNet.MvcCoreWebUI

AcademicWordNet.MvcCoreWebUI, akademik topluluklar için özel olarak tasarlanmış kapsamlı bir sözlük web sitesidir, özellikle eğitim kurumlarındaki öğrencilere odaklanmıştır. Platform, eksisozluk.com'a benzer özellikler sunar ancak akademik ortama özel ek özelliklerle donatılmıştır.

## Temel Özellikler

- **Öğrenciye Özel**: Erişim ve katkılar yalnızca kayıtlı öğrencilere özeldir.
- **E-posta Şifre Sıfırlama**: Öğrenciler, şifrelerini e-posta yoluyla sıfırlayabilirler.
- **Yönetici Paneli**: Yöneticiler, öğrencileri yönetebilir, Excel dosyaları aracılığıyla toplu kayıt yapabilir ve giriş kodlarını otomatik olarak oluşturabilir.
- **Kişiye Özel Erişim**: Her öğrenciye kişisel erişim için benzersiz bir giriş kodu sağlanır.
- **Küfür Filtresi**: Platform, uygun olmayan dilin kullanımını engellemek için yönetici panelinde küfür filtresi içerir.
- **Başlık Onayı**: Öğrenciler konu önerilerinde bulunabilir, ancak girişlerin görünür olması için yönetici onayı gerekir.
- **Giriş Yönetimi**: Öğrenciler, onaylanan konular altında özgürce giriş yapabilirler.

## Başlarken

AcademicWordNet.MvcCoreWebUI'yi kullanmaya başlamak için şu adımları izleyin:

1. Bu deposunu yerel makinenize klonlayın.
2. Visual Studio veya tercih ettiğiniz IDE'de çözümü açın.
3. `context.cs` dosyasında veritabanı bağlantı dizesini yapılandırın.
4. `appsettings.json` dosyasında mail servisi için gerekli olan mail bilgilerini yapılandırın.
5. Gerekli tabloları oluşturmak için veritabanı göçlerini çalıştırın.
6. Uygulamayı derleyin ve çalıştırın.
7. Bir yönetici olarak, öğrenci kayıtlarını yönetin ve yönetici paneli aracılığıyla içerik kalitesini denetleyin.
