# Sistema-De-Tutorias
Sistema De Tutorias re-ingenieria


# Como Correr en DEV

Para proder ejecutar el proyecto deben cumplirse los siguientes requisitos 
- Utilizar la version 6.X.X  de .Net Core 
- Instalar las dependencias del proyecto 
	- se puede realizar ejecutando `dotnet restore` 
- Tener instalado uno de los siguientes servicios de Base de Datos 
	- MariaDb 
	- MySql


## Instalar de forma global el Framework Visual
Este proyecto fue construido sobre el framework de interfaces graficas [AvaloniaUI](https://docs.avaloniaui.net/) 
por lo tanto para poder ejecutar el proyecto es necesario instalar de forma global el framework para correr.

``` bash 
dotnet new --install Avalonia.Templates
```

**Nota:** en caso de utilizar la version 7 de dotnet se debera ejecutar `install` en lugar de `--install`

## Migrar la base de datos

**Prerequisitos**
1. Tener una base de datos database un usuario= usuario y un password=password en la posterioridad se cambiara evidentemente
2. En rider seleccionamos el proyecto de consola llamado migraciones
3. y ejecutamos el proyecto esto hara que las tablas esten en nuestra base de datos


3. 


