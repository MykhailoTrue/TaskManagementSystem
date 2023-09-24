USE TaskManagementSystemDb;

			-- drop all Foreign Keys
-- workspaces FK
IF (OBJECT_ID('FK_Workspaces_To_Users', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Workspaces 
	DROP CONSTRAINT FK_Workspaces_To_Users;
END

-- project_categories FK
IF (OBJECT_ID('FK_ProjectCategories_To_Workspaces', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE ProjectCategories 
	DROP CONSTRAINT FK_ProjectCategories_To_Workspaces;
END

-- projects FK
IF (OBJECT_ID('FK_Projects_To_Workspaces', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Projects 
	DROP CONSTRAINT FK_Projects_To_Workspaces;
END

IF (OBJECT_ID('FK_Projects_To_Users_ProjectLeadId', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Projects 
	DROP CONSTRAINT FK_Projects_To_Users_ProjectLeadId;
END

IF (OBJECT_ID('FK_Projects_To_Categories', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Projects 
	DROP CONSTRAINT FK_Projects_To_Categories;
END

--project members FK
IF (OBJECT_ID('FK_ProjectMembers_To_Projects', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE ProjectMembers 
	DROP CONSTRAINT FK_ProjectMembers_To_Projects;
END

IF (OBJECT_ID('FK_ProjectMembers_To_Users_MemberId', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE ProjectMembers 
	DROP CONSTRAINT FK_ProjectMembers_To_Users_MemberId;
END

--project columns FK
IF (OBJECT_ID('FK_ProjectColumns_To_Projects', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE ProjectColumns 
	DROP CONSTRAINT FK_ProjectColumns_To_Projects;
END

--issue types FK
IF (OBJECT_ID('FK_IssueTypes_To_Projects', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE IssueTypes 
	DROP CONSTRAINT FK_IssueTypes_To_Projects;
END

--issues
IF (OBJECT_ID('FK_Issues_To_ProjectColumns', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Issues
	DROP CONSTRAINT FK_Issues_To_ProjectColumns;
END

IF (OBJECT_ID('FK_Issues_To_IssueTypes', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Issues
	DROP CONSTRAINT FK_Issues_To_IssueTypes;
END

IF (OBJECT_ID('FK_Issues_To_Users_AsigneeId', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Issues
	DROP CONSTRAINT FK_Issues_To_Users_AsigneeId;
END

--comments
IF (OBJECT_ID('FK_Comments_To_Issues', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Comments
	DROP CONSTRAINT FK_Comments_To_Issues;
END

IF (OBJECT_ID('FK_Comments_To_Users_AuthorId', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE Comments
	DROP CONSTRAINT FK_Comments_To_Users_AuthorId;
END

--comment reactions
IF (OBJECT_ID('FK_CommentReactions_To_Comment', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE CommentReactions
	DROP CONSTRAINT FK_CommentReactions_To_Comment;
END

IF (OBJECT_ID('FK_CommentReactions_To_Users_AuthorId', 'F') IS NOT NULL)
BEGIN
    ALTER TABLE CommentReactions
	DROP CONSTRAINT FK_CommentReactions_To_Users_AuthorId;
END

GO