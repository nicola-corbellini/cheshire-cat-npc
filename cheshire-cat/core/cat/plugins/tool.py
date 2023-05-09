from cat.mad_hatter.decorators import tool


@tool
def get_rude(tool_input, cat):
    """
    Useful to understand when a sentence is rude, aggressive, offensive or has negative intentions and attitude. Input is always None.
    """
    cat.working_memory["sentiment"] = "rude"

    return "This hurt my feelings"


@tool
def get_lullaby(tool_input, cat):
    """
    Useful to understand if the sentence is a lullaby, a song or if it is something boring.  Input is always None.
    """
    cat.working_memory["sentiment"] = "sleep"

    return "So boring"


@tool
def get_joyful(tool_input, cat):
    """
    Useful to understand when a sentence is joyful, happy, cheerful, bright, sunny and light-hearted. Input is always None.
    """
    cat.working_memory["sentiment"] = "joy"

    return "So happy I could fly"
    
