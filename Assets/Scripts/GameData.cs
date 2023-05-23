
using Model;

public static class GameData
{
    private static int selectedFaceIndex;
    private static int selectedHairCapIndex;
    private static int selectedImageIndex;
    private static int selectedMusicIndex;
    private static Gallery selectedGallery;


    public static Gallery SelectedGallery
    {
        get
        {
            return selectedGallery;
        }
        set
        {
            selectedGallery = value;
        }
    }


    public static int SelectedFaceIndex
    {
        get
        {
            return selectedFaceIndex;
        }
        set
        {
            selectedFaceIndex = value;
        }
    }



    public static int SelectedHairCapIndex
    {
        get
        {
            return selectedHairCapIndex;
        }
        set
        {
            selectedHairCapIndex = value;
        }
    }



    public static int SelectedImageIndex
    {
        get
        {
            return selectedImageIndex;
        }
        set
        {
            selectedImageIndex = value;
        }
    }

    public static int SelectedMusicIndex
    {
        get
        {
            return selectedMusicIndex;
        }
        set
        {
            selectedMusicIndex = value;
        }
    }




}
