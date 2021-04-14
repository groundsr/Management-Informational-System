var participantIndex = 0
function addParticipant(event, value) {
    if (event.keyCode == 13) {
        event.preventDefault()
        var listItem = `<li class="participant" onclick="this.remove();removeParticipant('${participantIndex}')">
        ${value} <i class="fas fa-times"></i></li >`;
        $('#participantsList').append(listItem);
        $('#participantsContainer').append(`<input type="hidden" id="participant${participantIndex}" name="participants[${participantIndex}]" value="${value}" />`);
        participantIndex++;
        $('#participantEmail').val('');

    }
}

function removeParticipant(value) {
    document.getElementById("participant" + value).remove();
    for (let i = parseInt(value) + 1; i < participantIndex; i++) {    
        document.getElementById("participant" + i).setAttribute("name", "participants[" + (parseInt(i) - 1) + "]");
        document.getElementById("participant" + i).setAttribute("id", "participant" + (parseInt(i) - 1));
    }
    participantIndex--;
}