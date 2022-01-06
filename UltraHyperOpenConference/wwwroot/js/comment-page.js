$(document).ready(() => {
    let createCommentController = createCommentMethods()

    $('.comment-block').hide();
    $('.replay-message-button').click(createCommentController.onCommentClick);
    $('.comment-cancel-button').click(createCommentController.hideLastClicked);

    let editCommentController = createEditMethods()
    $('.edit-button').click(editCommentController.onEditClick);
    $('.cancel-edit-button').click(editCommentController.hideLastClicked);
    
    $('.ban-user-button').click(e => {
        let userId = $(e.target)
            .closest('.moder-block')
            .find(".comment-user-id")
            .val();
        $('#input-user-ban-id').val(userId);
        
        $('#banModal').modal({
            show: true,
            focus: true
        })
    });
});

function createCommentMethods() {
    let lastActiveCommentBlock = null;

    let hideLastClicked = () => {
        lastActiveCommentBlock.hide()
        lastActiveCommentBlock = null
        return false;
    }
    let onCommentClick = (e) => {
        console.log(e.target)

        let commentBlock = $(e.target)
            .closest('.media-body')
            .find(".comment-block")
            .first();
        console.log(commentBlock)

        if (lastActiveCommentBlock == null) {
            commentBlock.show()
            lastActiveCommentBlock = commentBlock;
        } else {
            if (lastActiveCommentBlock.is(commentBlock)) {
                commentBlock.hide()
                lastActiveCommentBlock = null
            } else {
                lastActiveCommentBlock.hide()
                commentBlock.show()
                lastActiveCommentBlock = commentBlock
            }
        }

        return false;
    };

    return {
        onCommentClick,
        hideLastClicked
    }
}

function createEditMethods() {
    let lastFocusCommentBlocks = null;
    let hideLastClicked = () => {
        if(lastFocusCommentBlocks == null)
            return;
        
        lastFocusCommentBlocks.messageText.show();
        lastFocusCommentBlocks.editButton.show();
        lastFocusCommentBlocks.messageEditTextArea.hide();
    }
    let onEditClick = (e) => {
        console.log(e.target)

        let messageText = $(e.target)
            .closest('.media-body')
            .find(".message-text")
            .first();
        let messageEditTextArea = $(e.target)
            .closest('.media-body')
            .find(".message-edit-text-area")
            .first();
        let editButton = $(e.target)
            .closest('.media-body')
            .find(".edit-button")
            .first();
        
        if(lastFocusCommentBlocks != null) {
            hideLastClicked()
        }
        
        messageText.hide();
        editButton.hide();
        messageEditTextArea.show();

        lastFocusCommentBlocks = {
            messageText,
            editButton,
            messageEditTextArea
        };
    }

    return {
        onEditClick,
        hideLastClicked
    }
}

