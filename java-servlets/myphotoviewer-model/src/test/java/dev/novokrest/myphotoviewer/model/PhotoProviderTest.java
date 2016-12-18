package dev.novokrest.myphotoviewer.model;

import com.google.common.collect.Lists;
import junit.framework.Assert;
import org.junit.Test;

import java.util.ArrayList;
import java.util.Collection;

public class PhotoProviderTest {

    @Test
    public void testGetPhotos() {
        final int photosCount = 100;

        PhotosProvider photosProvider = PhotosProvider.getInstance();
        ArrayList<Photo> photos = Lists.newArrayList(photosProvider.getPhotos());

        Assert.assertTrue(photos.size() == photosCount);
        printPhotos(photos);
    }

    private void printPhotos(Collection<Photo> photos) {
        photos.stream().forEach(System.out::println);
    }
}
