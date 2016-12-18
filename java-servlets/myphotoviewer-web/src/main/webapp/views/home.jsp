<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>

<!DOCTYPE html>
<html>
    <head>
        <style>
            body {
                margin: 0;
            }

            ul.nav-bar {
                list-style-type: none;
                margin: 0;
                padding: 0;
                width: 100%;
                background-color: #f1f1f1;
                height: 100px;
                overflow:auto;
            }

            ul.nav-bar li {
                display: inline-block;
            }

            li a {
                display: block;
                color: #000;
                padding: 8px 16px;
                text-decoration: none;
            }

            li a.active {
                background-color: #4CAF50;
                color: white;
            }

            li a:hover:not(.active) {
                background-color: #555;
                color: white;
            }

            div.photos {
                margin-left: 25%;
                padding: 1px 16px;
                height: 1000px;
            }

            div.photo {

            }
        </style>
    </head>
    <body>
        <ul class="nav-bar">
            <c:forEach items="${years}" var="year">
                <li><a href="#">${year}</a></li>
            </c:forEach>
        </ul>
        <div class="photos">
            <c:forEach items="${images}" var="image">
                <div class="photo">
                    <img src="${pageContext.request.contextPath}/images?id=${image.id}">
                </div>
            </c:forEach>
        </div>
    </body>
</html>
