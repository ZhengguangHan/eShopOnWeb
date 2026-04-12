#!/bin/bash
# create-plan.sh - Helper script to generate new spec-driven development plan files
# Usage: ./create-plan.sh "Feature Summary"

set -e

# Resolve paths relative to script location for portability across Cursor and Claude Code
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
PLANS_DIR="docs/plans"
TEMPLATE_FILE="$SCRIPT_DIR/../templates/feature-spec.md"

if [ $# -eq 0 ]; then
    echo "Usage: $0 \"Feature Summary\""
    echo "Example: $0 \"User Registration\""
    exit 1
fi

FEATURE_SUMMARY="$1"
mkdir -p "$PLANS_DIR"

LAST_ID=$(ls -1 "$PLANS_DIR"/PLAN-*.md 2>/dev/null | \
    sed 's/.*PLAN-\([0-9]*\)-.*/\1/' | \
    sort -n | tail -1)

if [ -z "$LAST_ID" ]; then
    NEXT_ID="001"
else
    NEXT_ID=$(printf "%03d" $((10#$LAST_ID + 1)))
fi

KEBAB_SUMMARY=$(echo "$FEATURE_SUMMARY" | \
    tr '[:upper:]' '[:lower:]' | \
    tr ' ' '-' | \
    sed 's/[^a-z0-9-]//g' | \
    sed 's/--*/-/g' | \
    sed 's/^-//;s/-$//')

PLAN_FILE="$PLANS_DIR/PLAN-$NEXT_ID-$KEBAB_SUMMARY.md"

if [ ! -f "$TEMPLATE_FILE" ]; then
    echo "Error: Template file not found at $TEMPLATE_FILE"
    exit 1
fi

cp "$TEMPLATE_FILE" "$PLAN_FILE"

CURRENT_DATE=$(date '+%Y-%m-%d')
CURRENT_TIMESTAMP=$(date '+%Y-%m-%d %H:%M:%S')

if [[ "$OSTYPE" == "darwin"* ]]; then
    sed -i '' "s/{id}/$NEXT_ID/g" "$PLAN_FILE"
    sed -i '' "s/{Feature Name}/$FEATURE_SUMMARY/g" "$PLAN_FILE"
    sed -i '' "s/{date and time}/$CURRENT_TIMESTAMP/g" "$PLAN_FILE"
    sed -i '' "s/{date}/$CURRENT_DATE/g" "$PLAN_FILE"
    sed -i '' "s/{timestamp}/$CURRENT_TIMESTAMP/g" "$PLAN_FILE"
else
    sed -i "s/{id}/$NEXT_ID/g" "$PLAN_FILE"
    sed -i "s/{Feature Name}/$FEATURE_SUMMARY/g" "$PLAN_FILE"
    sed -i "s/{date and time}/$CURRENT_TIMESTAMP/g" "$PLAN_FILE"
    sed -i "s/{date}/$CURRENT_DATE/g" "$PLAN_FILE"
    sed -i "s/{timestamp}/$CURRENT_TIMESTAMP/g" "$PLAN_FILE"
fi

echo "✅ Created plan file: $PLAN_FILE"
echo ""
echo "Plan ID: $NEXT_ID"
echo "Feature: $FEATURE_SUMMARY"
echo "File: $PLAN_FILE"

if command -v code &> /dev/null; then
    code "$PLAN_FILE"
elif command -v cursor &> /dev/null; then
    cursor "$PLAN_FILE"
fi

exit 0
