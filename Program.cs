using System.IO;

String nombre, ruta;
int opcion = 0;
if (!Directory.Exists("Archivos"))
{
    Directory.CreateDirectory("Archivos");
}


do{
    Console.WriteLine("===================");
    Console.WriteLine("        Menú:       ");
    Console.WriteLine("===================");
    Console.WriteLine("1. Crear un nuevo fichero de texto.");
    Console.WriteLine("2. Añadir registros en un fichero.");
    Console.WriteLine("3. Eliminar todos los registros de un fichero.");
    Console.WriteLine("4. Eliminar un fichero.");
    Console.WriteLine("5. Visualizar un fichero.");
    Console.WriteLine("6. Cerrar la aplicación. ");
    
    bool error = true;
    do
    {
        Console.WriteLine(" ");
        Console.WriteLine("Seleccione una opción: ");
        String opt = Console.ReadLine();
        Console.WriteLine(" ");

        try
        {
            opcion = int.Parse(opt);
            error = false;
        }
        catch
        {
            Console.WriteLine(" ");
            Console.WriteLine("ERROR: Escribe un número válido");
        }
        
    }while(error);

    switch (opcion)
    {
        case 1: //1. Crear un nuevo fichero con el nombre que el usuario le pase por consola.
            nombre = pedirNombreFich("Nombre del fichero (sin extensión):");
            ruta = crearRuta(nombreValido(nombre)); //obligamos a que el nombre sea valido
            if (ficheroNoExiste(ruta))
            {
                crearFichero(ruta);
            }
            break;
        case 2: //2. Añadir registros en el fichero.
            nombre = pedirNombreFich("Nombre del fichero que quieres abrir (sin extensión):");
            ruta = crearRuta(nombre);
            if (ficheroExiste(ruta))
            {
                Console.WriteLine("Escribe el registro:");
                String registro = Console.ReadLine();
                escribir(registro, ruta);
            }
            break;
        case 3: //3. Eliminar todos los registros del fichero.
            nombre = pedirNombreFich("Nombre del fichero del que quieres eliminar todos los registros (sin extensión):");
            ruta = crearRuta(nombre);
            if (ficheroExiste(ruta))
            {
                crearFichero(ruta);
            }
            break;
        case 4: //4. Eliminar el fichero.
            nombre = pedirNombreFich("Nombre del fichero que quieres eliminar (sin extensión):");
            ruta = crearRuta(nombre);
            if (ficheroExiste(ruta))
            {
                borrarFichero(ruta);
            }
            break;
        case 5:   //5. Visualizar los registro
            nombre = pedirNombreFich("Nombre del fichero que quieres visualizar (sin extensión): ");
            ruta = crearRuta(nombre);
            if (ficheroExiste(ruta))
            {
                leer(ruta);
            }
            break;
        case 6: //6. Cerrar la aplicación.
            Console.WriteLine("Cerrando la aplicación");
            break;
        default:
            Console.WriteLine("Por favor elige una de las opciones del menú.");
            break;
    } 
}while(opcion != 6);  //6. Cerrar la aplicación.


/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////

String pedirNombreFich(String texto) 
{
    Console.WriteLine(texto);
    return Console.ReadLine();
}

String crearRuta(String nombre)
{
    return Path.Combine("./Archivos", nombre + ".txt");
}

void crearFichero(String ruta) 
{
    try
    {
        FileStream archivo = File.Create(ruta);
        archivo.Close();
        Console.WriteLine("Archivo creado exitosamente.");
    }catch (Exception ex) {
        Console.WriteLine("Error al crear archivo: " + ex.Message);
    }
}

void leer(String ruta)
{
    String[] lineas = File.ReadAllLines(ruta).ToArray();
    
    if (lineas.Length == 0)
    {
        Console.WriteLine("El fichero no tiene registros.");
    }
    else
    {
        Console.WriteLine("------ Registro Fichero: ------");
        foreach (String linea in lineas)
        {
            Console.WriteLine(linea);
        }
        Console.WriteLine("--------------------------------");
    }
    
}

void escribir(String linea, String ruta)
{
    File.AppendAllText(ruta, linea + Environment.NewLine);
    
}

void borrarFichero (String ruta)
{
    if (ficheroExiste(ruta))
    {
        File.Delete(ruta);
        Console.WriteLine("Fichero eliminado.");
    }
}

bool ficheroExiste (String ruta)
{
    if (!File.Exists(ruta))
    {
        Console.WriteLine();
        Console.WriteLine("El fichero no existe.");
        Console.WriteLine();
        return false;
    }
    return true;
}

bool ficheroNoExiste (String ruta)
{
    if (File.Exists(ruta))
    {
        Console.WriteLine("El fichero ya existe.");
        return false;
    }
    return true;
}

String nombreValido(String nombre){
    bool InvalidChar = true;
    do
    {
        if(nombre.IndexOfAny(Path.GetInvalidFileNameChars())>=0)
        {
            nombre = pedirNombreFich("El nombre del fichero no puede contener caracteres invalidos (ej: /, :, *, ?, <, >, |), por favor introduce otro nombre:");
        }
        else InvalidChar=false;

    } while (InvalidChar);
    return nombre;
}
                




