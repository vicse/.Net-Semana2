use neptuno;

CREATE PROCEDURE Usp_ListaAnios
as
select distinct year(fechapedido) as Anios from Pedidos

go;

exec Usp_ListaAnios;

create procedure Usp_Lista_Pedidos_Anios
@anio int
as
Select Idpedido,Nombrecompañia,Apellidos + ' ' + Nombre as Empleado,
FechaPedido,FechaEntrega
from Clientes inner join Pedidos
on Clientes.idcliente = Pedidos.Idcliente
inner join Empleados
on Pedidos.Idempleado = Empleados.Idempleado
where year(FechaPedido)=@anio

exec Usp_Lista_Pedidos_Anios'1996';

alter procedure Usp_Detalle_Pedido
@idpedido int
as
select detallesdepedidos.IdProducto,NombreProducto,detallesdepedidos.PrecioUnidad,
Cantidad, detallesdepedidos.PrecioUnidad*Cantidad as Monto
from detallesdepedidos inner join Productos
on detallesdepedidos.IdProducto=Productos.Idproducto
where idPedido=@idpedido

exec Usp_Detalle_Pedido'10760';

go;

SELECT * FROM Empleados;
SELECT * FROM Pedidos;

alter PROCEDURE Usp_ListaEmpleados
as
SELECT * from Empleados

EXEC Usp_ListaEmpleados;