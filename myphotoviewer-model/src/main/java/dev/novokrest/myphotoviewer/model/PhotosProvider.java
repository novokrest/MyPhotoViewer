package dev.novokrest.myphotoviewer.model;

import com.google.common.base.Function;
import com.google.common.collect.Iterators;
import dev.novokrest.myphotoviewer.util.filesystem.FileSystemEx;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.IOException;
import java.util.Iterator;


public class PhotosProvider {
    private static final String PhotosDirectory = "E:\\Github\\MyPhotoViewer\\photos-test";
    private static final Logger log = LogManager.getLogger(PhotosProvider.class);
    private static final Object lock = new Object();

    private static volatile PhotosProvider photosProviderInstance;

    public static PhotosProvider getInstance() {
        PhotosProvider localInstance = photosProviderInstance;
        if (localInstance == null) {
            synchronized (lock) {
                localInstance = photosProviderInstance;
                if (localInstance == null) {
                    photosProviderInstance = localInstance = new PhotosProvider(PhotosDirectory);
                }
            }
        }
        return photosProviderInstance;
    }

    private final String[] rootDirectories;

    private PhotosProvider(String... rootDirectories) {
        this.rootDirectories = rootDirectories;
    }

    public Iterator<Photo> getPhotos() {
        Iterator<String> filesIterator = FileSystemEx.listFilesInDirectories(rootDirectories);
        Iterator<Photo> photoIterator = Iterators.transform(filesIterator, new Function<String, Photo>() {
            @Override
            public Photo apply(String file) {
                try {
                    return Photo.createFromFile(file);
                } catch (IOException e) {
                    log.error("Failed to create photo from file: %s", file);
                    throw new RuntimeException(e);
                }
            }
        });

        return photoIterator;
    }
}
