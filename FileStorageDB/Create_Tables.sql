CREATE TABLE FileInformation (
    Id INTEGER PRIMARY KEY,
    FileName TEXT NOT NULL
);



CREATE TABLE Filestore (
    Id INTEGER PRIMARY KEY,
    Content BLOB NOT NULL,
    UploadedDate DATETIME NOT NULL,
    Version INTEGER NOT NULL
);
