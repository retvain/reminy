CREATE TABLE IF NOT EXISTS note_tags
(
    note_id BIGINT NOT NULL,
    tag_id  BIGINT NOT NULL,
    PRIMARY KEY (note_id, tag_id)
);