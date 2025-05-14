git fsck --lost-found | grep blob | cut -d' ' -f3 | while read hash; do
  echo "=== $hash ===" >> ntm-le-blob
  git show "$hash" >> ntm-le-blob
  echo
done
