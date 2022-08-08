namespace CookPC.VM {
    class Devices {
        public Drive localstorage;
        public Drive floppy_a;
        public Drive floppy_b;
    }

    class Drive {
        public int max;
        public Partition[] partitions;
    }

    class Partition {
        public int max;
    }
}