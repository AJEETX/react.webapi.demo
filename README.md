# reactjs plus .net core webapi crud sample  


[![.Net Framework](https://img.shields.io/badge/DotNet-2.1-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1) |[![Node](https://img.shields.io/badge/NodeJs-blue.svg?style=plastic)](https://nodejs.org/en/download/) | ![GitHub language count](https://img.shields.io/github/languages/count/ajeetx/sample.demo.svg) | ![GitHub top language](https://img.shields.io/github/languages/top/ajeetx/sample.demo.svg) |![GitHub repo size in bytes](https://img.shields.io/github/repo-size/ajeetx/sample.demo.svg) 
| --- | ---          | ---        | ---      | ---        | 

---------------------------------------

## Repository codebase
 
The repository consists of projects as below:


| # |Project Name | Project detail | location|
| ---| ---  | ---           | ---          |
| 1 | WebApi | Asp.Net Core2 WebApi as backend  |  server folder |
| 2 | WebApi.Test | Unit Test for webapi |  test folder |
| 3 | sample.demo | reactjs in front end   | client folder |

### Summary

The overall objective of the applications :
```
>	A user can Register
>	Then the user can Login 
>	jwt authentication is used
>	Once logged-in, user can do "CRUD" operation
```

### Setup detail

##### Environment Setup detail

>   1. Download/install **[Dotnet core2.1](https://www.microsoft.com/net/download/dotnet-core/2.1)** to run webapi project
>   
>   2. Download/install **[Node](https://nodejs.org/en/download/)** to run the sample.demo [front end] application
>   
>	3. Download/install **[Visual Studio Code](https://code.visualstudio.com/)** to run/debug the applications
>	
>   4. Download/install **[git bash](https://git-scm.com/downloads)** and configure witin Visual Studio Code by putting `"terminal.integrated.shell.windows": "C:\\Program Files\\Git\\bin\\bash.exe"` in the **users setting**.Please verify the location of the `bash.exe` and modify as per its location
>   5. In Visual Studio Code, please install a **[C# extension](https://github.com/OmniSharp/omnisharp-roslyn)**
>   

>   

##### Project Setup detail

>   1. Please clone or download the repository from **[github](https://github.com/AJEETX/sample.demo)**
>   
>   2. Create a folder and place the downloaded repository
>   3. Open **Visual Studio Code** and open the newly created folder where the repository is downloaded
>   
##### (a) To start the backend webapi service
   
>   1. Within **Visual Studio Code** open a command terminal by pressing the computer keyboard buttons `Control` and `~`
>    
>   2. Within the terminal, browse to folder location named as **"server"** 
>  
>   3. Restore the dependencies, type `dotnet restore` on the terminal
>
>   4. Run the webapi project, type `dotnet run` on the terminal
>   
>   5. **Webapi** [backend service] shall start running on port **5000**

##### (b) To start the front end application

>   1. Within **Visual Studio Code** Open a new command terminal
>   
>   2. Within the new terminal, browse to the folder named as **"client"**
>   
>   3. To restore the dependencies, type `npm install` on the terminal
>   
>   4. Now in order to run the sample.demo (front end application), type `npm start` on the terminal
>   
>   5. Shortly a browser shall open with url as `localhost:8080`

```
For better experience please chrome browser
```

##### (c) To run the unit test project
>   1. Within **Visual Studio Code** Open a new command terminal
>   
>   2. Within the new terminal, browse to the folder named as **"test"**
>   
>   3. To run the tests, type `dotnet test` on the terminal



### Support or Contact

Having any trouble? Please read out this [documentation](https://github.com/AJEETX/sample.demo/blob/master/README.md) or [contact](mailto:ajeetkumar@email.com) and to sort it out.

 [![HitCount](http://hits.dwyl.io/ajeetx/sample.demo/projects/1.svg)](http://hits.dwyl.io/ajeetx/sample.demo/projects/1) | ![GitHub contributors](https://img.shields.io/github/contributors/ajeetx/sample.demo.svg?style=plastic)|
 | --- | --- |

