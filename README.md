# Developer Evaluation Project

`READ CAREFULLY`

## Instructions
**The test below will have up to 7 calendar days to be delivered from the date of receipt of this manual.**

- The code must be versioned in a public Github repository and a link must be sent for evaluation once completed
- Upload this template to your repository and start working from it
- Read the instructions carefully and make sure all requirements are being addressed
- The repository must provide instructions on how to configure, execute and test the project
- Documentation and overall organization will also be taken into consideration

## Use Case
**You are a developer on the DeveloperStore team. Now we need to implement the API prototypes.**

As we work with `DDD`, to reference entities from other domains, we use the `External Identities` pattern with denormalization of entity descriptions.

Therefore, you will write an API (complete CRUD) that handles sales records. The API needs to be able to inform:

* Sale number
* Date when the sale was made
* Customer
* Total sale amount
* Branch where the sale was made
* Products
* Quantities
* Unit prices
* Discounts
* Total amount for each item
* Cancelled/Not Cancelled

It's not mandatory, but it would be a differential to build code for publishing events of:
* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

If you write the code, **it's not required** to actually publish to any Message Broker. You can log a message in the application log or however you find most convenient.

### Business Rules

* Purchases above 4 identical items have a 10% discount
* Purchases between 10 and 20 identical items have a 20% discount
* It's not possible to sell above 20 identical items
* Purchases below 4 items cannot have a discount

These business rules define quantity-based discounting tiers and limitations:

1. Discount Tiers:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount

2. Restrictions:
   - Maximum limit: 20 items per product
   - No discounts allowed for quantities below 4 items

## Overview
This section provides a high-level overview of the project and the various skills and competencies it aims to assess for developer candidates. 

See [Overview](/.doc/overview.md)

## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.doc/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.doc/frameworks.md)

<!-- 
## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](./docs/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)
-->

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.doc/project-structure.md)

## Running the Project with Docker

You can run the entire solution (API, database, cache, etc.) using Docker Compose. Make sure you have Docker and Docker Compose installed on your machine.

### 1. Build and Start the Containers

From the root of the repository (where the `docker-compose.yml` file is located), run:

```sh
docker-compose build --no-cache
docker-compose up -d
```

This will build the images and start all services in the background.

### 2. Accessing the API

- The API will be available at: [http://localhost:8080](http://localhost:8080)
- The Swagger UI (API documentation) will be available at: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

### 3. Stopping the Containers

To stop all running containers:

```sh
docker-compose down
```

<img width="1540" height="384" alt="image" src="https://github.com/user-attachments/assets/dfe41a07-4665-4e82-80b4-c8d7eb80270b" />

<img width="1157" height="366" alt="image" src="https://github.com/user-attachments/assets/867b699c-a261-45f0-b97c-9ef80fd9e729" />

### 4. Troubleshooting

- Ensure the environment variables in `docker-compose.yml` are correctly indented and set.
- If you make code changes, rebuild the images with `docker-compose build --no-cache`.
- Check logs with `docker-compose logs -f` for troubleshooting.
