# RescueEquip Web Shop

RescueEquip is a web shop application for selling mountaineering and rescue equipment, developed as a graduation project. The application demonstrates a complete e-commerce workflow, including product browsing, cart management, and order processing, with a focus on modularity, security, and responsive design.

---

## Features

- **Product Catalog:** Browse products by categories and subcategories, dynamically loaded from the database.
- **Product Details:** View detailed information, images, and prices for each product.
- **Cart Management:** Add, remove, and update product quantities in the cart, with real-time updates via AJAX.
- **Order Process:** Guided checkout with address and delivery method input.
- **Search Functionality:** Search for products by name or description.
- **Responsive Design:** Fully functional on desktop and mobile devices.
- **SEO Optimization:** Clean URLs and meta tags for better search engine visibility.
- **Modular Components:** Reusable code for headers, footers, and product listings.

---

## Technologies Used

**Frontend:**
- HTML5, CSS3
- Bootstrap
- JavaScript, jQuery
- Font Awesome
- Owl Carousel

**Backend:**
- ASP.NET Web Forms (VB.NET)
- ADO.NET (database access)
- SQL Server
- Stored Procedures
- Session and Cookies

---

## Project Structure


---

## How It Works

- **Frontend** pages use modular components from `Komponente.vb` to render headers, footers, and product lists.
- **AJAX** is used for cart operations and order processing, providing a seamless user experience without full page reloads.
- **Database** operations are performed via ADO.NET and stored procedures, ensuring security and performance.
- **Session** is used to track the user's cart and (optionally) login state.
- **URL formatting** and SEO are handled by utility functions (e.g., `SrediNaziv`) to ensure clean, user-friendly links.

---

## Setup and Configuration

1. **Requirements:**
   - Visual Studio 2019 or newer
   - SQL Server (local or remote)
   - .NET Framework (compatible with ASP.NET Web Forms)

2. **Database:**
   - Create a SQL Server database and run the provided schema/scripts (not included here).
   - Update the connection string in `Web.config` under `<connectionStrings>` (key: `conZavrsni`).

3. **Running the Project:**
   - Open the solution in Visual Studio.
   - Build and run the project (F5).
   - The application will be available at `http://localhost:[port]/`.

4. **Configuration:**
   - Adjust settings and meta tags in `Komponente.vb` as needed.
   - Add product images and slider images to the appropriate folders.

---

## Author

- Zvonimir Kožul

---

*This project was developed as a demonstration of web application development skills, database integration, and user experience design.*
