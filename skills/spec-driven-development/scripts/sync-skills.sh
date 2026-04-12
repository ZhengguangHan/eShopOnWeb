#!/bin/bash
# sync-skills.sh - Copy skills from repo to user-level directory based on environment
# Usage: ./sync-skills.sh [--dry-run] [--help]

set -e

# Resolve script directory and skills root
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
REPO_SKILLS_DIR="$(cd "$SCRIPT_DIR/../.." && pwd)"

# Parse arguments
DRY_RUN=false
for arg in "$@"; do
    case "$arg" in
        --dry-run)
            DRY_RUN=true
            ;;
        --help|-h)
            echo "Usage: $(basename "$0") [--dry-run] [--help]"
            echo ""
            echo "Copy skill directories from the repo skills/ folder to the"
            echo "user-level skills directory (~/.cursor/skills or ~/.claude/skills)."
            echo ""
            echo "Options:"
            echo "  --dry-run  Show what would be copied without making changes"
            echo "  --help     Show this help message"
            exit 0
            ;;
    esac
done

echo "📦 Syncing skills from: $REPO_SKILLS_DIR"

# Detect environment
if [ -d "$HOME/.cursor/skills" ]; then
    TARGET_DIR="$HOME/.cursor/skills"
    ENV="Cursor"
elif [ -d "$HOME/.claude/skills" ]; then
    TARGET_DIR="$HOME/.claude/skills"
    ENV="Claude Code"
else
    echo "❌ Error: Neither ~/.cursor/skills nor ~/.claude/skills directory found."
    echo "Please create the appropriate directory for your environment first."
    exit 1
fi

echo "🔍 Detected environment: $ENV"
echo "📂 Target directory: $TARGET_DIR"

if [ "$DRY_RUN" = true ]; then
    echo ""
    echo "🔍 Dry run — would copy:"
    for skill_dir in "$REPO_SKILLS_DIR"/*; do
        if [ -d "$skill_dir" ]; then
            echo "  → $(basename "$skill_dir")"
        fi
    done
    echo ""
    echo "No changes made."
    exit 0
fi

# Confirm with user
read -p "Copy skills from repo to $TARGET_DIR? (y/N): " -n 1 -r
echo
if [[ ! $REPLY =~ ^[Yy]$ ]]; then
    echo "❌ Sync cancelled."
    exit 0
fi

# Copy all skill directories
echo "📋 Copying skills..."
for skill_dir in "$REPO_SKILLS_DIR"/*; do
    if [ -d "$skill_dir" ]; then
        skill_name=$(basename "$skill_dir")
        echo "  → $skill_name"
        cp -r "$skill_dir" "$TARGET_DIR/"
    fi
done

echo "✅ Skills synced successfully to $TARGET_DIR"
echo ""
echo "Synced skills:"
ls -1 "$TARGET_DIR" | sed 's/^/  - /'
