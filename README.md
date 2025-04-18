# **RMALEKAR.COM.NP : OVERVIEW**

This project is source code of my developer portfolio website [Rehman's Portfolio Website](https://www.rmalekar.com.np)

This whole project is built on **__Microsoft Visual Studio 2022__** IDE and has two project solutions:


**1. RMFrontEndPro** :

__This is React App which is based on JavaScript. It fetch the required data from JSON File stored in public directory of the project.__


**2. RMalekar** :

__This Solution has three projects. They are :__


**a. RMalekarAPi**: 

__This is the ASP.NET C# Core Web API project built on *.NET 8.0*. The RESTful API is accessible at [Rehman RESTFUL API](HTTPS://WEBAPI.RMALEKAR.COM.NP). Currently available RESTFUL APIs are:__

- /qualifications
- /skills
- /experiences
- /portfolios
- /certifications

This project also defines a **__background service__** which is executed every 15 days to fetch data from MySQL Database and update **JSON Object File** stored on **public dir** of frontend repo.


**b. RMalekarDataContext** :

__This project is used to manage connection with database which usage **IServiceCollection Interface** to register database context.__

**c. RMalekarEntityModels** :

__This project defines database entity models for tables and views.__

## REQUIRED CONFIGURATION TO RUN THE PROJECT
1. RMFrontEndPro

	This project requires URL to RESTful API to send message which is hard coded in __/rc/assets/components/sections/Contact.jsx__

	Otherthan this, for version 1.0.0 of this project, we don't require any other configuration although you may find appsettings.json file with contents which is actually no use at the moment.

2. RMalekarAPI

   - This Project needs to setup following Env. Variables
     	a. RMALEKAR_WEBAPP_DB_SERVER
     	b. RMALEKAR_WEBAPP_DB_NAME
     	c. MYSQL_USER
     	d. MYSQL_PASSWORD

   - This Project needs following Configuration Varaible stored in appsettings.json file

   	1. "Server": "smtp_server"
 
    	2. "Port": "port number"
        
    	3. "Username": "email@domain.com"
        
    	4. "AppPassword": "app_password"
    
    	5. "FromEmail": "email@domain.com"

   - This Project needs following private readonly class values to setup required for GIT Push in [GitHubJsonUpdater.cs](https://github.com/devrmalekar/rmalekarwebsite/blob/main/RMalekar/RMalekarAPI/Services/GitHubJsonUpdater.cs)
        a. private readonly string _owner = "owner"
     
        b. private readonly string _repo = "repo"
     
        c. private readonly string _pat = "pat"
     
        d. private readonly string _branch = "branch"

## Other Features
Domain has it's own email setup configured correctly with required **DMRAC**, **SPF**, **DKIM** and **MX** records in DNS to make the email address genuine and not a spam message as well as to forward email to my primary email account.



## System Architecture

<img width="1498" alt="sys arch" src="https://github.com/user-attachments/assets/8e659d0e-b2c6-4d1f-9fd9-cb8d23206671" />




## **Authors and Acknowledgment**

Created by **[Rehman Malekare](https://www.rmalekar.com.np)**.
