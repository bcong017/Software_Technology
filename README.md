
Sửa file App.config ở dòng này:
connectionString="metadata=res://*/Model.Model1.csdl|res://*/Model.Model1.ssdl|res://*/Model.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=THESTARGAZER\THESTARGAZER;initial catalog=GARAGE_MANAGEMENT;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
Sửa data source=THESTARGAZER\THESTARGAZER thành Server name ở bảng Connect to Server khi mới mở SQL server lên

CẬP NHẬT DB LẠI 
CẬP NHẬT DB MỚI NHẤT