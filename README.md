
<p align="center"><h1 align="center">E-SHOP MİCROSERVICE PROJECT</h1></p>
<p align="center">
	<!-- Shields.io badges disabled, using skill icons. --></p>
<p align="center">Built with the tools and technologies:</p>
<p align="center">
	<a href="https://skillicons.dev">
		<img src="https://skillicons.dev/icons?i=,cs,dotnet,&theme=light">
	</a></p>
<br>

## 🔗 Table of Contents

- [📍 Overview](#-overview)
- [👾 Features](#-features)
- [📁 Project Structure](#-project-structure)
  - [📂 Project Index](#-project-index)
- [🚀 Getting Started](#-getting-started)
  - [☑️ Prerequisites](#-prerequisites)
  - [⚙️ Installation](#-installation)
  - [🤖 Usage](#🤖-usage)
---

## 📍 Overview

The "ETicaretProjesi" (E-Commerce Project) is a comprehensive solution developed using C# and ASP.NET Core. It is structured into multiple projects, each serving a distinct role within the application

- ETicaretProjesi.Core: Contains core models and shared functionalities.
- EticaretProjesi.API: Acts as the main API project, handling HTTP requests and responses.
- MyServices: Provides auxiliary services, such as token management and HTTP client services.
- PaymentAPI: Manages payment-related operations and integrations.
- PaymentAPI.Core: Houses core models and services specific to the PaymentAPI.
---

## 👾 Features

1. Modular Architecture: The solution is divided into distinct projects, promoting separation of concerns and enhancing maintainability.

2. Comprehensive API Layer: The EticaretProjesi.API project includes controllers for various entities:

    - AccountController.cs: Manages user account operations.
    - ProductController.cs: Handles product-related functionalities.
    - CategoryController.cs: Manages product categories.
    - CartController.cs: Oversees shopping cart operations.
    - PaymentController.cs: Facilitates payment processes.

3. Entity Framework Integration: Utilizes Entity Framework for data access, with a DatabaseContext class defining the data model and managing database interactions.

4. Payment Processing: The PaymentAPI project is dedicated to handling payment transactions, ensuring secure and efficient payment processing.

5. Shared Services: The MyServices project offers shared services like TokenServices for token management and HttpClientService for making HTTP requests, promoting code reuse across the application.

6. Core Models: The ETicaretProjesi.Core and PaymentAPI.Core projects define essential models such as CategoryModel, ProductModel, CartModel, AccountModel, and various payment-related models, ensuring a clear and organized data structure.
---

## 📁 Project Structure

```sh
└── ETicaretProjesi/
    ├── ETicaretProjesi.Core
    │   ├── ETicaretProjesi.Core.csproj
    │   ├── Models
    │   └── Resp.cs
    ├── EticaretProjesi.API
    │   ├── Controllers
    │   ├── DataAccess
    │   ├── Entities
    │   ├── EticaretProjesi.API.csproj
    │   ├── Migrations
    │   ├── Program.cs
    │   ├── Properties
    │   ├── Startup.cs
    │   ├── appsettings.Development.json
    │   └── appsettings.json
    ├── EticaretProjesi.sln
    ├── MyServices
    │   ├── HttpClientService.cs
    │   ├── MyServices.csproj
    │   └── TokenServices.cs
    ├── PaymentAPI
    │   ├── Controllers
    │   ├── PaymentAPI.csproj
    │   ├── Program.cs
    │   ├── Properties
    │   ├── Startup.cs
    │   ├── appsettings.Development.json
    │   └── appsettings.json
    └── PaymentAPI.Core
        ├── Model
        └── PaymentAPI.Core.csproj
```


### 📂 Project Index
<details open>
	<summary><b><code>ETICARETPROJESI/</code></b></summary>
	<details> <!-- PaymentAPI Submodule -->
		<summary><b>PaymentAPI</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI/PaymentAPI.csproj'>PaymentAPI.csproj</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI/Startup.cs'>Startup.cs</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI/appsettings.Development.json'>appsettings.Development.json</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI/appsettings.json'>appsettings.json</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI/Program.cs'>Program.cs</a></b></td>
			</tr>
			</table>
			<details>
				<summary><b>Controllers</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI/Controllers/PayController.cs'>PayController.cs</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Properties</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI/Properties/launchSettings.json'>launchSettings.json</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
	<details> <!-- ETicaretProjesi.Core Submodule -->
		<summary><b>ETicaretProjesi.Core</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/ETicaretProjesi.Core/Resp.cs'>Resp.cs</a></b></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/ETicaretProjesi.Core/ETicaretProjesi.Core.csproj'>ETicaretProjesi.Core.csproj</a></b></td>
			</tr>
			</table>
			<details>
				<summary><b>Models</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/ETicaretProjesi.Core/Models/CategoryModel.cs'>CategoryModel.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/ETicaretProjesi.Core/Models/PayModel.cs'>PayModel.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/ETicaretProjesi.Core/Models/ProductModel.cs'>ProductModel.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/ETicaretProjesi.Core/Models/CartModel.cs'>CartModel.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/ETicaretProjesi.Core/Models/AccountModel.cs'>AccountModel.cs</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
	<details> <!-- PaymentAPI.Core Submodule -->
		<summary><b>PaymentAPI.Core</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI.Core/PaymentAPI.Core.csproj'>PaymentAPI.Core.csproj</a></b></td>
			</tr>
			</table>
			<details>
				<summary><b>Model</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI.Core/Model/PaymentResponseModel.cs'>PaymentResponseModel.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI.Core/Model/AuthRequestModel.cs'>AuthRequestModel.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI.Core/Model/PaymentRequestModel.cs'>PaymentRequestModel.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/PaymentAPI.Core/Model/AuthResponseModel.cs'>AuthResponseModel.cs</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
	<details> <!-- MyServices Submodule -->
		<summary><b>MyServices</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/MyServices/TokenServices.cs'>TokenServices.cs</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/MyServices/HttpClientService.cs'>HttpClientService.cs</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/MyServices/MyServices.csproj'>MyServices.csproj</a></b></td>
			</tr>
			</table>
		</blockquote>
	</details>
	<details> <!-- EticaretProjesi.API Submodule -->
		<summary><b>EticaretProjesi.API</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Startup.cs'>Startup.cs</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/appsettings.Development.json'>appsettings.Development.json</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/appsettings.json'>appsettings.json</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/EticaretProjesi.API.csproj'>EticaretProjesi.API.csproj</a></b></td>
				<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Program.cs'>Program.cs</a></b></td>
			</tr>
			</table>
			<details>
				<summary><b>Migrations</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Migrations/20220602112405_initial-create.cs'>20220602112405_initial-create.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Migrations/20220602112405_initial-create.Designer.cs'>20220602112405_initial-create.Designer.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Migrations/DatabaseContextModelSnapshot.cs'>DatabaseContextModelSnapshot.cs</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Controllers</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Controllers/PaymentController.cs'>PaymentController.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Controllers/ProductController.cs'>ProductController.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Controllers/AccountController.cs'>AccountController.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Controllers/CartController.cs'>CartController.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Controllers/CategoryController.cs'>CategoryController.cs</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Properties</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Properties/launchSettings.json'>launchSettings.json</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>DataAccess</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/DataAccess/DatabaseContext.cs'>DatabaseContext.cs</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Entities</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Entities/Account.cs'>Account.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Entities/Category.cs'>Category.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Entities/Payment.cs'>Payment.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Entities/CartProduct.cs'>CartProduct.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Entities/Cart.cs'>Cart.cs</a></b></td>
						<td><b><a href='https://github.com/hakanereenn/ETicaretProjesi/blob/master/EticaretProjesi.API/Entities/Product.cs'>Product.cs</a></b></td>
					</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
</details>


---
## 🚀 Getting Started

### ☑️ Prerequisites

Before getting started with ETicaretProjesi, ensure your runtime environment meets the following requirements:

- **Programming Language:** CSharp
- **Package Manager:** Nuget


### ⚙️ Installation

Install ETicaretProjesi using one of the following methods:

**Build from source:**

1. Clone the ETicaretProjesi repository:
```sh
❯ git clone https://github.com/hakanereenn/ETicaretProjesi
```

2. Navigate to the project directory:
```sh
❯ cd ETicaretProjesi
```

3. Install the project dependencies:


**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet restore
```




### 🤖 Usage
Run ETicaretProjesi using the following command:
**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet run
```

