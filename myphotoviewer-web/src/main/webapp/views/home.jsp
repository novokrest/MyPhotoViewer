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
        <c:forEach items="${images}" var="image">
            <div class="photo">
                <img src="${pageContext.request.contextPath}/images?id=${image.id}">
            </div>
        </c:forEach>
    </body>
</html>
