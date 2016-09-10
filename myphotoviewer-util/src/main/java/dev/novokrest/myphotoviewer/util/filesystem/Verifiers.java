package dev.novokrest.myphotoviewer.util.filesystem;


public class Verifiers {

    public static void Verify(boolean b) {
        if (!b) {
            throw new RuntimeException();
        }
    }

    public static void Verify(boolean b, String message, Object... args) {
        if (!b) {
            throw new RuntimeException(String.format(message, args));
        }
    }
}
