package dev.novokrest.myphotoviewer.web;

import com.google.common.collect.Lists;
import dev.novokrest.myphotoviewer.model.Photo;
import dev.novokrest.myphotoviewer.model.PhotoYearsProvider;
import dev.novokrest.myphotoviewer.model.PhotosProvider;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;


public class HomeServlet extends HttpServlet {
    private static final Logger log = LogManager.getLogger(HomeServlet.class);

    @Override
    public void doGet(HttpServletRequest request,
                      HttpServletResponse response)
            throws IOException, ServletException {

        Collection<Integer> years = getPhotoYears();
        Collection<Image> images = getPhotoImages();

        request.setAttribute("years", years);
        request.setAttribute("images", images);
        RequestDispatcher dispatcher = request.getRequestDispatcher("views/home.jsp");
        dispatcher.forward(request, response);
    }

    private Collection<Integer> getPhotoYears() {
        PhotosProvider photosProvider = PhotosProvider.getInstance();
        PhotoYearsProvider yearsProvider = new PhotoYearsProvider(photosProvider);
        Collection<Integer> years = yearsProvider.getYears();
        return years;
    }

    private Collection<Image> getPhotoImages() {
        PhotosProvider photosProvider = PhotosProvider.getInstance();
        Collection<Photo> photos = Lists.newArrayList(photosProvider.getPhotos());

        final ImagesManager imagesManager = ImagesManager.getInstance();
        final List<Image> imageIds = new ArrayList<>(photos.size());
        photos.stream().forEach((Photo photo) -> imageIds.add(new Image(imagesManager.registerPhoto(photo))));

        return imageIds;
    }
}
