CREATE TABLE users (
    id BIGSERIAL,
    external_id TEXT NOT NULL UNIQUE,
    name TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE,
    created_at TIMESTAMPTZ DEFAULT NOW() NOT NULL,
    last_update TIMESTAMPTZ DEFAULT NOW() NOT NULL,
    is_deleted BOOLEAN DEFAULT FALSE NOT NULL,
    CONSTRAINT pk_users_id PRIMARY KEY (id)
);

CREATE INDEX idx_users_is_deleted ON users(is_deleted);
CREATE INDEX idx_users_external_id ON users(external_id);