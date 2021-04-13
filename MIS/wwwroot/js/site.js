var participantIndex = 0
function addParticipant(event, value) {
    if (event.keyCode == 13) {
        event.preventDefault()
        var listItem = `<li class="participant" onclick="this.remove()">
        ${value} <i class="fas fa-times"></i></li >`;
        $('#participantsList').append(listItem);
        $('#participantsList').append(`<input type="hidden" name="participants[${participantIndex}]" />`);
        participantIndex++;
        $('#participantEmail').val('');

    }
}