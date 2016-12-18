package dev.novokrest.myphotoviewer.web;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.FileInputStream;
import java.io.IOException;


public class ImagesServlet extends HttpServlet {
    private static final Logger log = LogManager.getFormatterLogger(ImagesServlet.class);
    private static final String IMAGE_PARAMETER = "id";

    @Override
    protected void doGet(HttpServletRequest request,
                         HttpServletResponse response)
        throws IOException {

        String photoPath = resolvePhotoPath(request);
        sendPhoto(photoPath, response);
    }

    private String resolvePhotoPath(HttpServletRequest request) {
        int photoId = extractPhotoId(request);
        String photoPath = ImagesManager.getInstance().getPhoto(photoId).getPath();
        log.info("id=%d, path=%s", photoId, photoPath);
        return photoPath;
    }

    private int extractPhotoId(HttpServletRequest request) {
        String imageId = request.getParameter(IMAGE_PARAMETER);
        int photoId = Integer.parseInt(imageId);
        return photoId;
    }

    private void sendPhoto(String photoPath, HttpServletResponse response)
            throws IOException {
        setAppropriateMimeType(photoPath, response);
    }

    private void setAppropriateMimeType(String photoPath, HttpServletResponse response)
            throws IOException {
        String mimeType = getServletContext().getMimeType(photoPath);
        if (mimeType == null) {
            response.setStatus(HttpServletResponse.SC_INTERNAL_SERVER_ERROR);
            return;
        }

        FileInputStream imageInputStream = null;
        ServletOutputStream outputStream = null;
        try {
            imageInputStream = new FileInputStream(photoPath);
            outputStream = response.getOutputStream();
            writeImageToResponse(imageInputStream, outputStream);
        }
        finally {
            if (imageInputStream != null) {
                imageInputStream.close();
            }
            if (outputStream != null) {
                outputStream.close();
            }
        }

    }

    private void writeImageToResponse(FileInputStream imageInputStream, ServletOutputStream outputStream)
            throws IOException {
        byte[] buffer = new byte[1024];
        int readBytes = 0;
        while ((readBytes = imageInputStream.read(buffer)) != -1) {
            outputStream.write(buffer, 0, readBytes);
        }
        outputStream.flush();
    }
}
