using System.Collections.Generic;
using Team1P2.Models.Models.Enums;

namespace Team1P2.Models.Models
{
  public class SeedData
  {
    public List<User> Users { get; set; } = new List<User>()
    {
      new User() {
        Username = "username1",
        Password = "password",
        ScreenName = "screenname1",
        Name = "name1"
      },
      new User() {
        Username = "username2",
        Password = "password",
        ScreenName = "screenname2",
        Name = "name2"
      },
      new User() {
        Username = "username3",
        Password = "password",
        ScreenName = "screenname3",
        Name = "name2"
      },
      new User() {
        Username = "username3",
        Password = "password",
        ScreenName = "screenname3",
        Name = "name2"
      }
    };

    public List<Blurb> Blurbs { get; set; } = new List<Blurb>()
    {
      new Blurb() {
        Message = "blurb1",
        Name="blurb1",
        Score = 3
      },
      new Blurb() {
        Message = "blurb2",
        Name="blurb2",
        Score = 5
      },
      new Blurb() {
        Message = "blurb3",
        Name="blurb3",
        Score = 7
      },
      new Blurb() {
        Message = "blurb4",
        Name="blurb4",
        Score = 9
      },
    };

    public List<Media> Medias { get; set; } = new List<Media>()
    {
      new Media() {
        Name="media1",
        Type = Type.Movie
      },
      new Media() {
        Name="media2",
        Type = Type.Book
      },
      new Media() {
        Name="media3",
        Type = Type.Game
      },
      new Media() {
        Name="media4",
        Type = Type.TV
      },
    };

    public List<Tag> Tags { get; set; } = new List<Tag>()
    {
      new Tag() {
        Name="tag1"
      },
      new Tag() {
        Name="tag2"
      },
      new Tag() {
        Name="tag3"
      },
      new Tag() {
        Name="tag4"
      },
    };

    public List<Note> Notes { get; set; } = new List<Note>()
    {
      new Note() {
        NoteBody="notebody1"
      },
      new Note() {
        NoteBody="notebody2"
      }
    };

  }
}
