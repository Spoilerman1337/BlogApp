# BlogApp

Attempt at creating a blog site for personal (or not so personal?) use

Project is divided into several layers according to Clean Architecture:
- Domain - Core of application: consist purely out of abstractions
- Application - Main logic of the app
- Infrastructure - Logic of connection to DB and such
- Presentation - Rudely speaking - View of the app

Also contains:
- Authorization - Authorization server
- Frontend - JS framework client app