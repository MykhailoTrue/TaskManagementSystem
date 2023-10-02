USE TaskManagementSystemDb;
			-- recreate table if exists
--Users
IF OBJECT_ID(N'Users', N'U') IS NOT NULL
	DROP TABLE Users;
CREATE TABLE dbo.Users
(
	Id INT IDENTITY(1,1),
	FirstName NVARCHAR(max) NOT NULL,
	LastName NVARCHAR(max) NOT NULL,
	Email NVARCHAR(50) UNIQUE NOT NULL,
	BirthDate DATE NULL,

	CONSTRAINT PK_User_Id PRIMARY KEY (Id),
);

--Workspaces
IF OBJECT_ID(N'Workspaces', N'U') IS NOT NULL
	DROP TABLE Workspaces;
CREATE TABLE Workspaces
(
	Id INT IDENTITY(1,1),
	Title NVARCHAR(max) NOT NULL,
	[Description] NVARCHAR(max) NULL,
	CreatedAt date NOT NULL,
	UpdatedAt date NOT NULL,
	AuthorId INT NOT NULL,
	
	CONSTRAINT PK_Workspace_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Workspaces_To_Users FOREIGN KEY (AuthorId) REFERENCES Users (Id)
		ON DELETE CASCADE,
);

-- ProjectCategories
IF OBJECT_ID(N'ProjectCategories', N'U') IS NOT NULL
	DROP TABLE ProjectCategories
CREATE TABLE ProjectCategories
(
	Id INT IDENTITY(1,1),
	[Name] NVARCHAR(max) NOT NULL,
	[Description] NVARCHAR(max) NULL,
	WorkspaceId INT NOT NULL,

	CONSTRAINT PK_ProjectCategories_Id PRIMARY KEY(Id),
	CONSTRAINT FK_ProjectCategories_To_Workspaces FOREIGN KEY (WorkspaceId) REFERENCES Workspaces (Id)
		ON DELETE CASCADE,
)

-- Projects
IF OBJECT_ID(N'Projects', N'U') IS NOT NULL
	DROP TABLE Projects
CREATE TABLE Projects
(
	Id INT IDENTITY(1,1),
	[Name] NVARCHAR(max) NOT NULL,
	WorkspaceId INT NOT NULL,
	ProjectCategoryId INT NULL,

	CONSTRAINT PK_Project_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Projects_To_Workspaces FOREIGN KEY (WorkspaceId) REFERENCES Workspaces (Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_Projects_To_Categories FOREIGN KEY (ProjectCategoryId) REFERENCES ProjectCategories (Id)
		ON DELETE NO ACTION,
);

-- Project Members
IF OBJECT_ID(N'ProjectMembers', N'U') IS NOT NULL
	DROP TABLE ProjectMembers
CREATE TABLE ProjectMembers
(
	Id INT IDENTITY(1,1),
	ProjectId INT NOT NULL,
	MemberId INT NOT NULL,

	CONSTRAINT PK_ProjectMembers PRIMARY KEY(Id),
	CONSTRAINT FK_ProjectMembers_To_Projects FOREIGN KEY (ProjectId) REFERENCES Projects (Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_ProjectMembers_To_Users_MemberId FOREIGN KEY (MemberId) REFERENCES Users (Id)
		ON DELETE NO ACTION,
)

-- Project Columns
IF OBJECT_ID(N'ProjectColumns', N'U') IS NOT NULL
	DROP TABLE ProjectColumns
CREATE TABLE ProjectColumns
(
	Id INT IDENTITY(1,1),
	[Name] NVARCHAR(max) NOT NULL,
	ProjectId INT NOT NULL,

	CONSTRAINT PK_ProjectColumns PRIMARY KEY(Id),
	CONSTRAINT FK_ProjectColumns_To_Projects FOREIGN KEY (ProjectId) REFERENCES Projects (Id)
		ON DELETE CASCADE,
)

-- Issue Types
IF OBJECT_ID(N'IssueTypes', N'U') IS NOT NULL
	DROP TABLE IssueTypes
CREATE TABLE IssueTypes
(
	Id INT IDENTITY(1,1),
	[Name] NVARCHAR(max) NOT NULL,
	[Description] NVARCHAR(max) NULL,
	ProjectId INT NOT NULL,

	CONSTRAINT PK_IssueTypes PRIMARY KEY(Id),
	CONSTRAINT FK_IssueTypes_To_Projects FOREIGN KEY (ProjectId) REFERENCES Projects (Id)
		ON DELETE CASCADE,
)

-- Issues
IF OBJECT_ID(N'Issues', N'U') IS NOT NULL
	DROP TABLE Issues
CREATE TABLE Issues
(
	Id INT IDENTITY(1,1),
	[Name] NVARCHAR(max) NOT NULL,
	[Descritption] NVARCHAR(max) NULL,
	ProjectColumnId INT NOT NULL,
	IssueTypeId INT NOT NULL,
	AsigneeId INT NOT NULL,


	CONSTRAINT PK_Issues PRIMARY KEY(Id),
	CONSTRAINT FK_Issues_To_ProjectColumns FOREIGN KEY (ProjectColumnId) REFERENCES ProjectColumns (Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_Issues_To_IssueTypes FOREIGN KEY (IssueTypeId) REFERENCES IssueTypes (Id)
		ON DELETE NO ACTION,
	CONSTRAINT FK_Issues_To_Users_AsigneeId FOREIGN KEY (AsigneeId) REFERENCES Users (Id)
		ON DELETE NO ACTION,
)

-- Comments
IF OBJECT_ID(N'Comments', N'U') IS NOT NULL
	DROP TABLE Comments
CREATE TABLE Comments
(
	Id INT IDENTITY(1,1),
	Body NVARCHAR(max) NOT NULL,
	CreatedAt DATE NULL,
	UpdatedAt DATE NULL,

	IssueId INT NOT NULL,
	AuthorId INT NOT NULL,


	CONSTRAINT PK_Comments PRIMARY KEY(Id),
	CONSTRAINT FK_Comments_To_Issues FOREIGN KEY (IssueId) REFERENCES Issues (Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_Comments_To_Users_AuthorId FOREIGN KEY (AuthorId) REFERENCES Users (Id)
		ON DELETE NO ACTION,
)

-- Comment Reactions
IF OBJECT_ID(N'CommentReactions', N'U') IS NOT NULL
	DROP TABLE CommentReactions
CREATE TABLE CommentReactions
(
	Id INT IDENTITY(1,1),
	IsLike BIT NOT NULL,
	CreatedAt DATE NULL,
	UpdatedAt DATE NULL,

	CommentId INT NOT NULL,
	AuthorId INT NOT NULL,


	CONSTRAINT PK_CommentReactions PRIMARY KEY(Id),
	CONSTRAINT FK_CommentReactions_To_Comment FOREIGN KEY (CommentId) REFERENCES Comments (Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_CommentReactions_To_Users_AuthorId FOREIGN KEY (AuthorId) REFERENCES Users (Id)
		ON DELETE NO ACTION,
)


GO