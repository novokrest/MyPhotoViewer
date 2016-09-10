package dev.novokrest.myphotoviewer.web;

import dev.novokrest.myphotoviewer.model.PhotoYearsProvider;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Collection;


public class HomeServlet extends HttpServlet {

    @Override
    public void doGet(HttpServletRequest request,
                      HttpServletResponse response)
            throws IOException, ServletException {

        PhotoYearsProvider yearsProvider = new PhotoYearsProvider();
        Collection<String> years = yearsProvider.getYears();

        request.setAttribute("years", years);
        RequestDispatcher dispatcher = request.getRequestDispatcher("views/home.jsp");
        dispatcher.forward(request, response);
    }
}
