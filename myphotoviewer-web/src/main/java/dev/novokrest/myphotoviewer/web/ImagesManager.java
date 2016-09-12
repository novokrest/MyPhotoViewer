package dev.novokrest.myphotoviewer.web;


import dev.novokrest.myphotoviewer.model.Photo;

import java.util.HashMap;
import java.util.Map;

public class ImagesManager {
    private static final Object lock = new Object();
    private static ImagesManager imagesManagerInstance;

    private final Map<Integer, Photo> idToPhotoMap = new HashMap<Integer, Photo>();
    private final Map<Photo, Integer> photoToIdMap = new HashMap<Photo, Integer>();
    private int availablePhotoId = 0;

    public static ImagesManager getInstance() {
        ImagesManager localInstance = imagesManagerInstance;
        if (localInstance == null) {
            synchronized (lock) {
                localInstance = imagesManagerInstance;
                if (localInstance == null) {
                    imagesManagerInstance = localInstance = new ImagesManager();
                }
            }
        }
        return localInstance;
    }

    private ImagesManager() {

    }

    public Photo getPhoto(int photoId) {
        return idToPhotoMap.get(photoId);
    }

    public int registerPhoto(Photo photo) {
        if (isPhotoAlreadyRegistered(photo)) {
            return getPhotoId(photo);
        }

        int photoId = savePhoto(photo);
        return photoId;
    }

    private boolean isPhotoAlreadyRegistered(Photo photo) {
        return photoToIdMap.containsKey(photo);
    }

    private int getPhotoId(Photo photo) {
        return photoToIdMap.get(photo);
    }

    private int savePhoto(Photo photo) {
        int photoId = getNewPhotoId();

        photoToIdMap.put(photo, photoId);
        idToPhotoMap.put(photoId, photo);

        return photoId;
    }

    private int getNewPhotoId() {
        return availablePhotoId++;
    }




}
