$(document).ready(() => {
    let commentController = createCommentController()

    $('.comment-block').hide();
    $('.replay-message-button').click(commentController.onCommentClick);
    $('.comment-cancel-button').click(commentController.hideLastClicked);
});

function createCommentController() {
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

        if(lastActiveCommentBlock == null)
        {
            commentBlock.show()
            lastActiveCommentBlock = commentBlock;
        }
        else
        {
            if(lastActiveCommentBlock.is(commentBlock))
            {
                commentBlock.hide()
                lastActiveCommentBlock = null
            }
            else
            {
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