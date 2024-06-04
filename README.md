# Git Workflow Rules

## General Rules
1. **Branch Naming Conventions**:
   - Use descriptive names starting with `feature_` for new features and `art_` for art-related tasks.
   - Example: `feature_user-authentication`, `art_character-design`.

2. **Commit Messages**:
   - Write clear and concise commit messages.
   - Use the format: `[Category] Short description`
   - Example: `[Fix] Corrected login issue`, `[Art] Updated character textures`.

3. **Pull Requests (PRs)**:
   - All changes must be made via pull requests.
   - Link PRs to relevant issues or tasks in Notion.
   - Provide a clear description of what the PR does and any relevant information for reviewers.

## For Developers
1. **Branch Creation**:
   - Create branches from the `main` branch for new features.
   - Keep your feature branch up-to-date by regularly merging changes from `main`.

2. **Code Reviews**:
   - All code must be reviewed before merging.
   - Ensure all conversations and requested changes are resolved before merging the PR.

3. **Testing**:
   - Test your code thoroughly before creating a PR. 
   - Writing unit tests is optional and should be decided based on the specific needs of the project.

4. **Branch Protection**:
   - Ensure your branch follows the naming conventions.
   - Resolve all conversations in PRs before merging.
   - Use signed commits if required.

## For Artists
1. **Branch Creation**:
   - Create branches from the `main` branch for new art assets.
   - Use the `art_` prefix for branch names.
   - Example: `art_new-character-sprites`.

2. **Asset Management**:
   - Organize art assets in appropriate directories.
   - Include any necessary documentation or instructions for using the assets.

3. **Pull Requests**:
   - Create a PR for any new or updated art assets.
   - Provide a description and any relevant details or references for the assets.

4. **Branch Protection**:
   - Follow the branch naming conventions.
   - Ensure all conversations in PRs are resolved before merging.

## Pull Request (PR) Guidelines
1. **Creating a PR**:
   - Ensure your branch is up-to-date with the latest changes from `main`.
   - Provide a clear and concise title and description.
   - Link to relevant issues or tasks in Notion.
   - Add screenshots or examples if applicable (especially for art assets).

2. **Review Process**:
   - At least one other team member must review the PR.
   - Address any comments or requested changes promptly.
   - Ensure all conversations are resolved before merging.

3. **Merging a PR**:
   - Only merge a PR after it has been approved and all conversations are resolved.
   - Follow the protected branch rules to ensure a clean and linear history.
   - Delete the branch after merging if it is no longer needed.

## Branch Protection Rules Recap
- **Require a pull request before merging**: Enabled
- **Require conversation resolution before merging**: Enabled
- **Require signed commits**: Optional (recommended for additional security)
- **Require linear history**: Enabled
- **Do not allow bypassing the above settings**: Enabled
- **Allow force pushes**: Disabled
- **Allow deletions**: Optional (based on workflow needs)

---

By following these rules, both developers and artists can maintain a structured and efficient workflow while ensuring the quality and consistency of the project.
