namespace dncCnt.Persistence.MySql
{
    public static class MysqlInit
    {
        /// <summary>
        /// mysql init script
        /// </summary>
        public static string Sql = @"
            CREATE TABLE IF NOT EXISTS `counter` (
                `guid` VARBINARY(16) NOT NULL,
                `value` BIGINT(20) NOT NULL,
                `ts` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                UNIQUE INDEX `idx` (`guid`)
            )
            ENGINE=InnoDB
        ";
    }
}