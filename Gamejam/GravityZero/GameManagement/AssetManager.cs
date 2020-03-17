using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;

public class AssetManager
{
    protected ContentManager contentManager;
    public List<Song> soundtrack = new List<Song>();
    public AssetManager(ContentManager content)
    {
        contentManager = content;
    }

    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
        {
            return null;
        }
        return contentManager.Load<Texture2D>(assetName);
    }

    public SpriteFont GetSpriteFont(string assetName)
    {
        if (assetName == "")
        {
            return null;
        }
        return contentManager.Load<SpriteFont>(assetName);
    }

    public void PlaySound(string assetName)
    {
        //SoundEffect snd = contentManager.Load<SoundEffect>(assetName);
        //snd.Play();
    }
    public void AddMusic(string assetName)
    {
        if (soundtrack.Count() < 50)
        {
            Song music = Content.Load<Song>(assetName);
            soundtrack.Add(music);
        }

    }
    public void PlaySong()
    { 
        if (soundtrack.Count() > 0)
        {
            MediaPlayer.Play(soundtrack[0]);
            soundtrack.Remove(soundtrack[0]);
        }
    }
    public void PlayMusic(string assetName, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(contentManager.Load<Song>(assetName));
    }

    public ContentManager Content
    {
        get { return contentManager; }
    }
}