#!/usr/bin/env bash
# Script to test Artist, Song, and Listener endpoints as implemented in PR #14
# Uses seeded test accounts and entities as per initializer context.
# Requires: curl, jq

API_BASE="https://localhost:5001"  # Change PORT if needed

# Test users from seeder
ADMIN_EMAIL="administrator@localhost"
ADMIN_PASS="Administrator1!"
ARTIST_EMAIL="artist@example.com"
ARTIST_PASS="Artist1!"
LISTENER_EMAIL="listener@example.com"
LISTENER_PASS="Listener1!"

# Login as admin and get token for authorized API calls
login() {
  local email="$1"
  local pass="$2"
  TOKEN=$(curl -k -s -X POST "$API_BASE/api/Users/login" \
    -H "Content-Type: application/json" \
    -d "{\"email\": \"$email\", \"password\": \"$pass\"}" | jq -r '.accessToken')
  if [[ "$TOKEN" == "null" || -z "$TOKEN" ]]; then
    echo "Failed to login as $email"
    exit 1
  fi
  echo "Logged in as $email"
}

# Helper for authenticated curl
auth_curl() {
  curl -k -s -w "\nHTTP %{http_code}\n" -H "Authorization: Bearer $TOKEN" "$@"
}

# Use artist by seeded values
SEED_ARTIST_NAME="Test Artist"
SEED_ARTIST_USERNAME="testartist"
SEED_ARTIST_EMAIL="artist@example.com"
SEED_LISTENER_NAME="Test Listener"
SEED_LISTENER_USERNAME="testlistener"
SEED_LISTENER_EMAIL="listener@example.com"
SEED_SONG_TITLE="Test Song"

###########################
# Authenticate as Admin
###########################
login "$ADMIN_EMAIL" "$ADMIN_PASS"

echo "---- ARTISTS: Get all (paginated) ----"
auth_curl "$API_BASE/api/Artists?pageNumber=1&pageSize=10"

echo "---- ARTISTS: Get by ID (assuming id=1) ----"
auth_curl "$API_BASE/api/Artists/1"

echo "---- ARTISTS: Get by name ----"
auth_curl "$API_BASE/api/Artists/by-name?name=$(printf "%s" "$SEED_ARTIST_NAME" | sed 's/ /%20/g')"

echo "---- ARTISTS: Create ----"
auth_curl -X POST "$API_BASE/api/Artists" \
  -H "Content-Type: application/json" \
  -d "{\"name\": \"New Artist\", \"username\": \"newartistuser\", \"email\": \"newartist@example.com\", \"bio\": \"Bio from script\", \"payoutTier\": \"Silver\", \"userId\": null, \"password\": \"ArtistTest123!\"}"

echo "---- ARTISTS: Update (id=1) ----"
auth_curl -X PUT "$API_BASE/api/Artists/1" \
  -H "Content-Type: application/json" \
  -d "{\"id\": 1, \"name\": \"Test Artist Updated\", \"bio\": \"Bio updated from script\"}"

echo "---- ARTISTS: Update payout tier (id=1) ----"
auth_curl -X PUT "$API_BASE/api/Artists/payout-tier/1" \
  -H "Content-Type: application/json" \
  -d "{\"artistId\": 1, \"payoutTier\": \"Gold\"}"

# For destructive testing, uncomment:
echo "---- ARTISTS: Delete (id=1) ----"
auth_curl -X DELETE "$API_BASE/api/Artists/1"

###########################
# LISTENERS
###########################
echo "---- LISTENERS: Get all (paginated) ----"
auth_curl "$API_BASE/api/Listeners?pageNumber=1&pageSize=10"

echo "---- LISTENERS: Get by username ----"
auth_curl "$API_BASE/api/Listeners/by-username?username=$SEED_LISTENER_USERNAME"

echo "---- LISTENERS: Create ----"
auth_curl -X POST "$API_BASE/api/Listeners" \
  -H "Content-Type: application/json" \
  -d "{\"name\": \"New Listener\", \"username\": \"newlisteneruser\", \"email\": \"newlistener@example.com\", \"userId\": null, \"password\": \"ListenerTest123!\"}"

echo "---- LISTENERS: Update (id=1) ----"
auth_curl -X PUT "$API_BASE/api/Listeners/1" \
  -H "Content-Type: application/json" \
  -d "{\"id\": 1, \"name\": \"Test Listener Updated\"}"

echo "---- LISTENERS: Update favorite song (listenerId=1, songId=1) ----"
auth_curl -X PUT "$API_BASE/api/Listeners/favorite-song" \
  -H "Content-Type: application/json" \
  -d "{\"listenerId\": 1, \"songId\": 1}"

# For destructive testing, uncomment:
echo "---- LISTENERS: Delete (id=1) ----"
auth_curl -X DELETE "$API_BASE/api/Listeners/1"


###########################
# SONGS
###########################
echo "---- SONGS: Get all (paginated) ----"
auth_curl "$API_BASE/api/Songs?pageNumber=1&pageSize=10"

echo "---- SONGS: Get by artist (seeded artistId=1) ----"
auth_curl "$API_BASE/api/Songs/by-artist?artistId=1&pageNumber=1&pageSize=10"

echo "---- SONGS: Create (artistId=1) ----"
auth_curl -X POST "$API_BASE/api/Songs" \
  -H "Content-Type: application/json" \
  -d "{\"title\": \"New Song\", \"artistId\": 1}"

echo "---- SONGS: Update (id=1) ----"
auth_curl -X PUT "$API_BASE/api/Songs/1" \
  -H "Content-Type: application/json" \
  -d "{\"id\": 1, \"title\": \"Updated Song Title\"}"

echo "---- SONGS: Play song (id=1) ----"
auth_curl -X PUT "$API_BASE/api/Songs/play/1"

# For destructive testing, uncomment:
echo "---- SONGS: Delete (id=1) ----"
auth_curl -X DELETE "$API_BASE/api/Songs/1"

echo "DONE."
