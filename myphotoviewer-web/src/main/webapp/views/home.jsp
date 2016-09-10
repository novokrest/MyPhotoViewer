<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>

<!DOCTYPE html>
<html>
    <body>
        <h1>PHOTOS</h1>
        <ul>
            <c:forEach items="${years}" var="year">
                <li><a href="">${year}</a></li>
            </c:forEach>
        </ul>
    </body>
</html>