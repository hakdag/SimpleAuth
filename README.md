![Build Ubuntu](https://github.com/hakdag/SimpleAuth/workflows/.NET%20Core/badge.svg?branch=master&event=push)
 
# SimpleAuth
Dotnet Core Based Simple Authentication & Authorization

Why to build same thing over and over again? Purpose of this project is to building a sperately working Authentication and Authorization REST based API with its own database.

## Table of Contents

- [Installation](#installation)
- [Features](#features)
- [Contributing](#contributing)
- [Team](#team)
- [FAQ](#faq)
- [Support](#support)
- [License](#license)

## Installation

- Clone this repo.
- Setup a database (prefer PostgreSQL).
- If you are using dotnet core in your project, also clone and integrate [SimpleAuth.Extensions](https://github.com/hakdag/SimpleAuth.Extensions)
- Update appsettings.json file in the Api project.
- Configure the TokenValidationParameters in the Startup.cs file for your requirements.

## Features

 - JWT Authentication
 - Users
 - Roles
 - Lock/Unlock user account
 - Password Reset
 - Limited login attempts

## Contributing

Create a Pull Request if you want to contribute.

## Team

| [Hakan Akdag](https://www.linkedin.com/in/hakanakdag/) | 

## FAQ



## Support

Reach out to me at one of the following places!

- LinkedIn at [https://www.linkedin.com/in/hakanakdag/](https://www.linkedin.com/in/hakanakdag/)
- Twitter at [https://twitter.com/akdaghakan](https://twitter.com/akdaghakan)

Or...
Create an issue.

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

- **[MIT license](http://opensource.org/licenses/mit-license.php)**
- Copyright 2020 © <a href="http://hakanakdag.net" target="_blank">Hakan Akdag</a>.
