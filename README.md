<h1 align="center">🌟 BookStore-E-Commerce 🌟</h1>
<p>Özet Açıklama :</p>
<p>.Net Core MVC Pattern kullanarak Backend tarafını oluşturdum, MSSQL kullanarak veri tabanını Codefirst yaklaşımı ile migrationları kullanarak tasarladım, 
Frontend tarafında Bootstrap kütüphanesini kullanarak yalın ve güncel bir şekilde tasarım oluşturdum, Identity kütüphanesiyle birlikte SMTP için SendGrid : https://sendgrid.com altyapısını kullanarak 
developerlar için sağlamış olduğu ücretsiz günlük 100 Request atabilme özelliği ile email API sini projeme dahil ettim ve kullanıcı mail onaylama, şifre değişikliği vb.
gibi alanlarda kullandım, Ödeme işlemleri için iyzico-iyzipay developerlar için sunmuş olduğu ücretsiz geliştirme apilerinden fake ödeme sistemlerini projeme dahil ettim.
 Projemin daha güzel bir görünüme sahip olaması için UI tarafında bazı sayfalarda Modal, Datatables ve Toastr kullandım.</p>
<h4 align="center">🟠 Genel Görünüm 🟠</h4>
</br>

![Giriş](https://user-images.githubusercontent.com/112801816/233827044-b8fdafd3-e366-408b-8b74-fde3ad29c689.png)

<h1 align="center">🟠 Roller ve işlemleri 🟠</h1>
<h2>🔶 Admin Rolü : </h2>
<p>• Admin Ekleme</p>
<p>• Kullanıcı Ekleme</p>
<p>• Kullanıcı Rolü Değiştirme</p>
<p>• Yazar CRUD işlemleri</p>
<p>• Kitap CRUD işlemleri</p>
<p>• Kategori CRUD işlemleri</p>
<p>• Sipariş yönetim işlemleri (Onaylama,Kargolama vb.)</p>
<h2>🔶 Kullanıcı Rolü : </h2>
<p>• Sipariş işlemleri</p>
<p>• Kullanıcı mail işlemleri</p>
<p>• Alışveriş Sepet işlemleri</p>
<p>• Kitap Arama işlemleri</p>
<p>• Satın Alma işlemleri</p>

<h1 align="center">🟠 Proje Görselleri ve Açıklamaları 🟠</h1>
</br>
<h2>🔶 Kullanıcı Üyelik işlemleri </h2>
<p>🟢 Açıklama : Aşağıdaki eklemiş olduğum video'da yapmış olduğum işlemleri sırası ile saniyelerini yazmış bulunmaktayım.</p>
<p>• Yeni Kullanıcı üye olma işlemi : Kullanıcı yazmış olduğum Custom Validationlar'a uyarak hesabını oluşturuyor.(00:00 - 00:57)</p>
<p>• Kullanıcı Mail doğrulama/onaylama işlemi : Kullanıcı sendgrid.com üzerinden SMTP desteği API'si ile gelen mail'deki link ile mail'ini doğruluyor.(00:57 - 01:56)</p>
<p>• Kullanıcı Şifremi Unuttum işlemi : Kullanıcı şifremi unuttum işlemini yapmak için mailine gelen bağlantı 
üzerinden erişim sağlayarak başarılı bir şekilde yeni bir şifre oluşturuyor.(01:56 - 02:50)</p>
<p>• Kullanıcı E-mail değiştirme işlemi : Kullanıcı gelen link üzerinden E-mailini değiştiriyor.(02:50 - 04:18)</p>
<p>• Kullanıcı Şifre Güncelle işlemi : Kullanıcı panelinden yeni şifre oluşturuyor.(04:18 - 05:28)</p>
<p>• Kullanıcı Hesap Silme işlemi : Kullanıcı panelinden hesabını siliyor.(05:28 - 06:02)</p>

<div align="center">

<a href="https://youtu.be/PWnREhcALAs" target="_blank">
    <img src="https://user-images.githubusercontent.com/112801816/233843615-e1c22c7b-8d2f-4d2d-a748-6937759f4d76.png" width="810";
     height="480" />
</a>

</div>

<h2>🔶 Kullanıcı sipariş işlemleri </h2>
<p>🟢 Açıklama : Aşağıdaki eklemiş olduğum video'da yapmış olduğum işlemleri sırası ile saniyelerini yazmış bulunmaktayım.</p>
<p>• Yeni Sipariş işlemi : Kullanıcı sepete ürün ekliyor, siliyor ve güncelliyor bütün işlemleri sepette yapabiliyor.(00:00 - 01:30)</p>
<p>• Sipariş Mail onay işlemi : Kullanıcı mailini onaylamadığını var sayarak alışveriş yapmak istediğinde maili onaylanmış olması gerekiyor 
bundan dolayı mailini onaylama işlemini yapıyor.(01:30 - 01:59)</p>
<p>• Sipariş Ödeme işlemi : Kullanıcı ödeme işlemini yapabilmesi için gerekli alanları eksiksiz doldurması gerekmektedir, burayı iyzico'nun geliştiricilere ücretsiz 
sunmuş olduğu API'yi kullanarak oluşturdum.(01:59 - 03:03)</p>
<p>• Sipariş Durum işlemi : Kullanıcı siparişin durumunu görüntülüyor hangi aşamada olduğunu görebiliyor.(03:03 - 03:30)</p>

<div align="center">

<a href="https://youtu.be/vnpJnwIhDVc" target="_blank">
    <img src="https://user-images.githubusercontent.com/112801816/233843615-e1c22c7b-8d2f-4d2d-a748-6937759f4d76.png" width="810";
     height="480" />
</a>

</div>

<h2>🔶 Admin Paneli işlemleri </h2>
<p>🟢 Açıklama : Aşağıdaki eklemiş olduğum video'da yapmış olduğum işlemleri sırası ile saniyelerini yazmış bulunmaktayım.</p>
<p>• Kategori CRUD işlemleri : Admin Kategorilerde ekleme, güncelleme, silme ve listeleme işlemlerini yapıyor.(00:00 - 00:40)</p>
<p>• Yazar CRUD işlemleri : Admin Yazarları ekleme, güncelleme, silme ve listeleme işlemlerini yapıyor.(00:40 - 01:18)</p>
<p>• Kitap CRUD işlemleri : Admin Custom Validation kurallarına göre Kitaplarda ekleme, güncelleme, silme ve listeleme işlemlerini yapıyor, eklediği kitabı arama çubuğunda arayarak 
arama işlemini gerçekleştiriyor ve kitapların pasif olma yada stok dışı olma durumunuda gösteriyor.(01:18 - 04:17)</p>
<p>• Kullanıcı CRUD işlemleri : Admin Custom Validation kullanıcı ekleme, silme ve listeleme işlemlerini yapıyor.(04:17 - 06:37)</p>
<p>• Sipariş Onay işlemleri : Admin Siparişlerin durumunu güncelleme işlemlerini yapıyor ve son olarak sipariş veren kullanıcı panelinde nasıl gözüktüğünü gösteriyor.
(06:37 - 08:05)</p>

<div align="center">

<a href="https://youtu.be/3KXysCJVbpc" target="_blank">
    <img src="https://user-images.githubusercontent.com/112801816/233843615-e1c22c7b-8d2f-4d2d-a748-6937759f4d76.png" width="810";
     height="480" />
</a>

</div>


<h2>🔶 Veri Tabanı Tablolar ve İlişkiler Şeması </h2>
<p>🟢 Açıklama : </p>
<p>• Sol tarafta bütün tablolar liste halinde gösterilmiştir, Veri tabanı, tablolar, sütünlar ve ilişkiler Codefirst yaklaşımıyla migration'ları kullanarak oluşturulmuştur.</p>
<p>• Bütün ilişkiler ve tablo adları ile içerdiği sütün adları resim üzerinde diagram olarak gösterilmiştir.</p>

![sql_diagram](https://user-images.githubusercontent.com/112801816/233906455-e6962156-d85a-4043-804b-9c989f928108.png)


<h2 align="center">🌟Proje'yi Yıldızlayıp(⭐) bana destekte bulunabilirsiniz..🌟</h5>
