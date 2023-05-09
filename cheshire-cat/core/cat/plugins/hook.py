from cat.mad_hatter.decorators import hook


@hook
def before_returning_response_to_user(response, cat):

    # Check if Sentiment key exists in Working Memory
    if "sentiment" in cat.working_memory.keys():

        # Add sentiment key to the final output
        response["sentiment"] = cat.working_memory["sentiment"]

        # Clean Working Memory Sentiment
        cat.working_memory.pop("sentiment")

    return response
